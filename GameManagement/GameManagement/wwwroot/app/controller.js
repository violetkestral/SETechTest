gameManagementApp.controller('IndexController', function ($scope, $route, dataService) {
    getAllGames();
    getAllPlatforms();

    $scope.deleteGame = function (gameId) {
        var promisePost = dataService.deleteGame(gameId);
        promisePost.then(function () {
            $route.reload();
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
                $scope.selectedPlatforms = res.platforms;
                $scope.isNewRecord = 0;
            },
            function (error) {
                console.log("Err" + error);
            });
    };

    $scope.saveGame = function () {
        var game = {
            Id: $scope.gameId,
            Title: $scope.title,
            Platforms: $scope.selectedPlatforms
        };

        var promisePost;
        if ($scope.isNewRecord === 1) {
            promisePost = dataService.addGame(game);
        } else {
            promisePost = dataService.editGame(game);
        };

        promisePost.then(function () {
            $route.reload();
        },
            function (error) {
                $scope.status = 'Error: ' + error;
                console.log("Err" + error);
            });
    };

    $scope.clear = function () {
        $scope.isNewRecord = 1;
        $scope.gameId = 0;
        $scope.title = "";
        $scope.selectedPlatforms = [];
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
});
//gameManagementApp.controller('EditController',
//    [
//        '$scope', 'game', 'platforms', 'dataService', 'errorsService',
//        function($scope, game, platforms, dataService, errorsService) {

//            var vm = this;

//            vm.game = game;
//            vm.platforms = checkPlatforms(platforms);
//            vm.hideEditForm = function() { $scope.go('games.details', { id: game.id }) };
//            vm.editGame = editGame;

//            function editGame() {
//                checkedPlatforms(vm.platforms);
//                dataService.editgame(vm.game)
//                    .then(function() {
//                            $scope.go('games.details', { id: vm.game.id });
//                        },
//                        function(errorResponse) {
//                            errorsService.throwResponseErrors(errorResponse);
//                        });
//            }

//            function checkedPlatforms(platforms) {
//                var selectedPlatforms = [];

//                window.angular.forEach(platforms,
//                    function(platform) {
//                        if (platform.isSelected) {
//                            selectedPlatforms.push({ gameId: vm.game.id, platformId: platform.id });
//                        }
//                    });

//                vm.game.platforms = selectedPlatforms;
//            }

//            function checkPlatforms(platforms) {
//                window.angular.forEach(platforms,
//                    function(platform) {
//                        if (gameOnPlatform(platform.id)) {
//                            platform.isSelected = true;
//                        } else {
//                            platform.isSelected = false;
//                        }
//                    });

//                return platforms;
//            }

//            function gameOnPlatform(platformId) {
//                if (typeof vm.game.platforms !== 'undefined') {
//                    return vm.game.platforms.some(function(e) {
//                        return e.platformId === platformId;
//                    });
//                }
//            }
//        }
//    ]);
