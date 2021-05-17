var newsFeedService =
    function (url) {
        this.retrieve = function () {
            return $.get({
                url: url,
                headers: {
                    accept: "application/json"
                }
            });
        }
    };

var newsFeedController =
    function NewsFeedApp($scope, newsFeedService) {
        $scope.stories = [];

        var init = function () {
            var success =
                function (data) {
                    var stories = JSON.parse(data);
                    $scope.$apply(function () {
                        $scope.stories=stories;
                    });
                }

            var error =
                function () {
                    console.log(error);
                }

            newsFeedService.retrieve().then(success, error);
        }

        init();
    }



var app = angular
    .module("newsFeedApp", [])
    .controller('NewsFeedController', ['$scope', 'newsFeedService', newsFeedController])
    .factory('newsFeedService', function () { return new newsFeedService('/api/NewsFeed'); })


    
    
    