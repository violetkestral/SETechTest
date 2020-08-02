gameManagementApp.service('dataService', function ($http) {

    var gamesRoutePrefix = "/api/games";

    this.getGames = function () {

        var requestUrl = gamesRoutePrefix + "/all";

        return $http({ method: "GET", url: requestUrl });
    };

    this.getGame = function (gameId) {

        var requestUrl = gamesRoutePrefix + "/game/" + gameId;

        return $http({
            method: "GET",
            url: requestUrl,
            transformResponse: function (data) {
                data = window.angular.fromJson(data);
                return data;
            }
        });
    };

    this.editGame = function (data) {

        var requestUrl = gamesRoutePrefix + "/edit";

        return $http({
            method: "POST",
            url: requestUrl,
            data: data,
            headers: { 'Content-Type': 'application/json' }
        });
    };

    this.addGame = function (data) {

        var requestUrl = gamesRoutePrefix + "/add";

        return $http({
            method: "POST",
            url: requestUrl,
            data: data,
            headers: { 'Content-Type': 'application/json' }
        });
    };

    this.deleteGame = function (gameId) {

        var requestUrl = gamesRoutePrefix + "/delete/" + gameId;

        return $http({
            method: "DELETE",
            url: requestUrl,
            headers: { 'Content-Type': 'application/json' }
        });
    };

    this.getPlatforms = function () {

        var requestUrl = "/api/platforms/all";

        return $http({ method: "GET", url: requestUrl });
    };
});