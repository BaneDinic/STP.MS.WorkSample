function EmployeeViewModel() {

    var self = this;
    var queryStringDict = parseQueryString();


    self.allExperienceLevels = ko.observableArray(['A', 'B', 'C', 'D']);

    self.name = ko.observable().extend({ requiredText: { overrideMessage: "Please enter an employee name", skipOnInit: true } });
    self.experienceLevel = ko.observable();
    self.startingDate = ko.observable().extend({ requiredDate: { overrideMessage: "Please enter a starting date", skipOnInit: true } });
    self.salary = ko.observable().extend({ numeric: 2 });
    self.vacationDays = ko.observable().extend({ numeric: 0 });
    self.companyId = ko.observable(queryStringDict['companyid']);
    self.companyName = ko.observable(queryStringDict['companyname']);

    self.AddEmployee = function () {

        $.ajax({
            type: "POST",
            dataType: "json",
            url: webapiUrl + 'api/employee',
            contentType: 'application/json',
            data: ko.toJSON(self),
            success: function (data) {

                location.href = '/employee/list?companyid=' + self.companyId() + '&companyname=' + queryStringDict['companyname'];
            }
        });
    };
};

var employeeViewModel = new EmployeeViewModel();
ko.applyBindings(employeeViewModel);