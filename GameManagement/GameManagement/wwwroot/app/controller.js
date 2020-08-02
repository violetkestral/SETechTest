gameManagementApp.controller('IndexController', function ($scope,dataService) {
    getAllGames();
    getAllPlatforms();

    $scope.isNewRecord = 1;

    $scope.clearScope = function() {
        clearForm();
    };

    $scope.deleteGame = function (gameId) {
        var promisePost = dataService.deleteGame(gameId);
        promisePost.then(function (result) {
            if (!result.data.success) {
                console.log("Err" + result.data.error);
            } else {
                getAllGames();
                clearForm();
            }
        },
            function (error) {
                console.log("Err" + error);
            });
    };

    $scope.getGame = function (gameId) {
        var promiseGetSingle = dataService.getGame(gameId);

        promiseGetSingle.then(
            function (result) {
                var res = result.data;
                $scope.gameId = res.id;
                $scope.title = res.title;
                $scope.isNewRecord = 0;
                setSelectedPlatforms(res.gamePlatforms);
            },
            function (error) {
                console.log("Err" + error);
            });
    };

    $scope.saveGame = function () {
        var platforms = getSelectedPlatforms();
        var game = {
            Id: $scope.gameId,
            Title: $scope.title,
            Platforms: platforms
        };

        var promisePost;
        if ($scope.isNewRecord === 1) {
            promisePost = dataService.addGame(game);
        } else {
            promisePost = dataService.editGame(game);
        };

        promisePost.then(function (result) {
            if (!result.data.success) {
                console.log("Err" + result.data.error);
            } else {
                getAllGames();
                clearForm();
            }
        },
            function (error) {
                $scope.status = 'Error: ' + error;
                console.log("Err" + error);
            });
    };

    function clearForm() {
        $scope.isNewRecord = 1;
        $scope.gameId = 0;
        $scope.title = "";
        $scope.platforms = getAllPlatforms();
    };

    function getAllGames() {
        var promise = dataService.getGames();

        promise.then(
            function (response) {
                $scope.gamesList = response.data;
                $scope.status = 'Loaded. Code: ' + response.status;
                $scope.statusClass = 'label-success';
            },
            function (error) {
                console.log("Err" + error);
            });
    };

    function getAllPlatforms() {
        var promise = dataService.getPlatforms();
        promise.then(
            function (response) {
                $scope.platforms = response.data;
                $scope.status = 'Loaded. Code: ' + response.status;
                $scope.statusClass = 'label-success';
            },
            function (error) {
                $scope.platforms = null;
                $scope.status = 'Error: ' + error;
                $scope.statusClass = 'label-warning';
            });
    };

    function getSelectedPlatforms() {
        var selectedPlatforms = [];

        window.angular.forEach($scope.platforms, function (platform) {
            if (platform.isSelected) {
                selectedPlatforms.push({ Id: platform.id });
            }
        });

        return selectedPlatforms;
    };

    function setSelectedPlatforms(gamePlatforms) {
        var allPlatforms = $scope.platforms;

        window.angular.forEach(allPlatforms, function (platform) {
            if (gameHasPlatform(gamePlatforms, platform.id)) {
                platform.isSelected = true;
            } else {
                platform.isSelected = false;
            }
        });
    }

    function gameHasPlatform(gamePlatforms, platformId) {
        if (gamePlatforms != undefined) {
            return gamePlatforms.some(function (e) {
                return e.platformId === platformId;
            });
        }

        return false;
    };
});