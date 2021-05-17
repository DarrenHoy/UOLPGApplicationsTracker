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
        $scope.isLoaded = false;

        $scope.openLink =
            function (url, ev) {
                window.open(url);
                ev.preventDefault();
            }

        var jsonDateToShortDateString =
            function (jsonDate) {
                var year = jsonDate.substring(0, 4);
                var month = jsonDate.substring(5, 7)
                var day = jsonDate.substring(8, 10)
                return day +"-" + month +"-" + year;
            }

        var formatData =
            function (story) {
                return {
                    headline: story.headline,
                    summary: story.summary,
                    date: story.date,
                    displayDate: jsonDateToShortDateString(story.date),
                    url: story.url,
                    tags: story.tags
                };
            }
        var init = function () {
            var success =
                function (data) {
                    var stories = $.map(JSON.parse(data), formatData);
                    $scope.$apply(function () {
                        $scope.stories = stories;
                        $scope.isLoaded = true;
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


    
    
    