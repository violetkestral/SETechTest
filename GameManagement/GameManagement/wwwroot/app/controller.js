gameManagementApp.controller("IndexController",
    function($scope, dataService) {
        getAllGames();
        getAllPlatforms();

        $scope.isNewRecord = 1;

        $scope.clearScope = () => {
            clearForm();
        };

        $scope.deleteGame = (gameId) => {
            if (gameId) {
                const promisePost = dataService.deleteGame(gameId);

                promisePost.then(
                    (result) => {
                        if (!result.data.success) {
                            console.log(`Err${result.data.error}`);
                        } else {
                            getAllGames();
                            clearForm();
                        }
                    },
                    (error) => {
                        console.log(`Err${error}`);
                    }
                );
            }
        };

        $scope.getGame = (gameId) => {
            if (gameId) {
                const promiseGetSingle = dataService.getGame(gameId);

                promiseGetSingle.then(
                    (result) => {
                        var res = result.data;
                        $scope.gameId = res.id;
                        $scope.title = res.title;
                        $scope.isNewRecord = 0;
                        setSelectedPlatforms(res.gamePlatforms);
                    },
                    (error) => {
                        console.log(`Err${error}`);
                    }
                );
            }
        };

        $scope.saveGame = () => {
            const platforms = getSelectedPlatforms();
            const game = {
                Id: $scope.gameId,
                Title: $scope.title,
                Platforms: platforms
            };

            let promisePost;
            if ($scope.isNewRecord === 1) {
                promisePost = dataService.addGame(game);
            } else {
                promisePost = dataService.editGame(game);
            };

            promisePost.then(
                (result) => {
                    if (!result.data.success) {
                        console.log(`Err${result.data.error}`);
                    } else {
                        getAllGames();
                        clearForm();
                    }
                },
                (error) => {
                    $scope.status = `Error: ${error}`;
                    console.log(`Err${error}`);
                }
            );
        };

        function clearForm() {
            $scope.isNewRecord = 1;
            $scope.gameId = 0;
            $scope.title = "";
            $scope.platforms = getAllPlatforms();
        };

        function getAllGames() {
            const promise = dataService.getGames();

            promise.then(
                (response) => {
                    $scope.gamesList = response.data;
                    $scope.status = `Loaded. Code: ${response.status}`;
                    $scope.statusClass = "label-success";
                },
                (error) => {
                    console.log(`Err${error}`);
                });
        };

        function getAllPlatforms() {
            const promise = dataService.getPlatforms();

            promise.then(
                (response) => {
                    $scope.platforms = response.data;
                    $scope.status = `Loaded. Code: ${response.status}`;
                    $scope.statusClass = "label-success";
                },
                (error) => {
                    $scope.platforms = null;
                    $scope.status = `Error: ${error}`;
                    $scope.statusClass = "label-warning";
                });
        };

        function getSelectedPlatforms() {
            const selectedPlatforms = [];

            window.angular.forEach($scope.platforms,
                (platform) => {
                    if (platform.isSelected) {
                        selectedPlatforms.push({ Id: platform.id });
                    }
                });

            return selectedPlatforms;
        };

        function setSelectedPlatforms(gamePlatforms) {
            if (gamePlatforms) {
                const allPlatforms = $scope.platforms;

                window.angular.forEach(allPlatforms,
                    (platform) => {
                        if (gameHasPlatform(gamePlatforms, platform.id)) {
                            platform.isSelected = true;
                        } else {
                            platform.isSelected = false;
                        }
                    });
            }
        }

        function gameHasPlatform(gamePlatforms, platformId) {
            if (gamePlatforms && gamePlatforms.length > 0 && platformId) {
                return gamePlatforms.some((e) => e.platformId === platformId);
            }
            return false;
        }
    });