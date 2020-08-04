gameManagementApp.service("dataService",
    function($http) {

        const gamesRoutePrefix = "/api/games";

        this.getGames = () => {
            const requestUrl = gamesRoutePrefix + "/all";

            return $http({
                method: "GET",
                url: requestUrl
            });
        };

        this.getGame = (gameId) => {
            const requestUrl = gamesRoutePrefix + "/game/" + gameId;

            return $http({
                method: "GET",
                url: requestUrl,
                transformResponse: (data) => {
                    return window.angular.fromJson(data);
                }
            });
        };

        this.editGame = (data) => {
            const requestUrl = gamesRoutePrefix + "/edit";

            return $http({
                method: "POST",
                url: requestUrl,
                data: data,
                headers: { 'Content-Type': "application/json" }
            });
        };

        this.addGame = (data) => {
            const requestUrl = gamesRoutePrefix + "/add";

            return $http({
                method: "POST",
                url: requestUrl,
                data: data,
                headers: { 'Content-Type': "application/json" }
            });
        };

        this.deleteGame = (gameId) => {
            var requestUrl = gamesRoutePrefix + "/delete/" + gameId;

            return $http({
                method: "DELETE",
                url: requestUrl,
                headers: { 'Content-Type': "application/json" }
            });
        };

        this.getPlatforms = () => {
            const requestUrl = "/api/platforms/all";

            return $http({
                method: "GET",
                url: requestUrl
            });
        };
    });