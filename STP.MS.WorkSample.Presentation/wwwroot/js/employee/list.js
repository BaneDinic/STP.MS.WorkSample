function EmployeeListViewModel() {
    var self = this;
    var queryStringDict = parseQueryString();

    self.companyName = ko.observable(queryStringDict['companyname']);

    self.items = ko.observableArray([]);

    self.GetEmployees = function () {

        var companyId = queryStringDict['companyid'];
        var pagingOptions = {

            pageIndex: self.employeeList.currentPageIndex(),
            pageSize: self.employeeList.pageSize
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            url: webapiUrl + 'api/employee/getbycompany?companyid=' + companyId,
            contentType: 'application/json',
            data: JSON.stringify(pagingOptions),
            success: function (data) {

                self.items(data.items);
                self.employeeList.manualMaxIndex(Math.ceil(data.totalCount / self.employeeList.pageSize) - 1);
            }
        });
    }


    var DeleteEmployee = function (itemId) {

        $.ajax({
            type: "DELETE",
            dataType: "json",
            url: webapiUrl + 'api/employee/' + itemId,
            success: function (data) {

                self.GetEmployees();
            }
        });
    };


    self.employeeList = new ko.simpleGrid.viewModel({
        data: self.items,
        columns: [
            { headerText: "Name", rowText: "name" },
            { headerText: "Exp. level", rowText: "experienceLevel" },
            {
                headerText: "Start date", rowText: {
                    date: function (item) {

                        return item.startingDate.split('T')[0];
                    }
                }
            },
            {
                headerText: "", rowText: {
                    edit: function (item) {
                        return function () {

                            location.href = '/employee/edit?employeeid=' + item.id;
                        }
                    }
                }
            },
            {
                headerText: "", rowText: {
                    delete: function (item) {
                        return function () {

                            DeleteEmployee(item.id);
                        }
                    }
                }
            }
        ],
        pageSize: 10,
        requestFunction: self.GetEmployees
    });


    self.GetEmployees();
};

var employeeListViewModel = new EmployeeListViewModel();
ko.applyBindings(employeeListViewModel);