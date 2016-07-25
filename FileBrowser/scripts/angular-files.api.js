var app = angular.module('FileBrowser', []);
var path = '..';
app.controller('FilesController', function filesController($scope, files) {

    files.searchFiles($scope.query, function (error, data) {
        if (!error) {
            $scope.filesInfo = data;
            $scope.path = path;
        }
    });

    $scope.openDirectory = function openDirectory(name) {
        files.getDirectory(name, function (error, data, name) {
            if (!error) {
                $scope.filesInfo = data;
                $scope.path = name;
            }
        });
    };
});

app.factory('files', function files($http) {
    return {
        searchFiles: function searchFiles(query, callback) {
            $http.get('http://localhost:52034/api/files?path=' + path)
            .success(function (data) {
                callback(null, data);
            })
            .error(function (e) {
                callback(e);
            });
        },
        getDirectory: function getDirectory(name, callback) {
            $http.get('http://localhost:52034/api/files?path=' + name)
                .success(function (data) {
                    callback(null, data, name);
                })
                .error(function (e) {
                    callback(e);
                });
        }
    };
});
