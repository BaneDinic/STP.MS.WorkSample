function EmployeeViewModel() {

    var self = this;
    var queryStringDict = parseQueryString();


    self.allExperienceLevels = ko.observableArray(['A', 'B', 'C', 'D']);

    self.employeeName = ko.observable();
    self.experienceLevel = ko.observable();
    self.startingDate = ko.observable().extend({ requiredDate: { overrideMessage: "Please enter a starting date" } });
    self.salary = ko.observable().extend({ numeric: 2 });
    self.vacationDays = ko.observable().extend({ numeric: 0 });
    self.companyName = ko.observable();

    self.GetEmployee = function () {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: webapiUrl + 'api/employee/' + queryStringDict['employeeid'],
            data: JSON.stringify(),
            success: function (data) {

                self.id = ko.observable(data.id);
                self.name = ko.observable(data.name);
                self.companyId = ko.observable(data.companyId);

                self.employeeName(data.name);
                self.experienceLevel(data.experienceLevel);
                self.salary(data.salary);
                self.vacationDays(data.vacationDays);
                self.companyName(data.companyName);
                self.startingDate(new Date(data.startingDate));
            }
        });
    }

    self.UpdateEmployee = function () {

        $.ajax({
            type: "PUT",
            dataType: "json",
            url: webapiUrl + 'api/employee',
            contentType: 'application/json',
            data: ko.toJSON(self),
            success: function (data) {

                location.href = '/employee/list?companyid=' + self.companyId() + '&companyname=' + self.companyName();
            }
        });
    };

    self.GetEmployee();
};

var employeeViewModel = new EmployeeViewModel();
ko.applyBindings(employeeViewModel);