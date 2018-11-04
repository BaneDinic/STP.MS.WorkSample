function CompanyListViewModel() {
    var self = this;

    self.items = ko.observableArray([]);
    self.itemToAdd = ko.observable("");

    self.GetCompanies = function () {

        var pagingOptions = {

            pageIndex: self.companyList.currentPageIndex(),
            pageSize: self.companyList.pageSize
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            url: webapiUrl + 'api/company/get',
            contentType: 'application/json',
            data: JSON.stringify(pagingOptions),
            success: function (data) {

                self.items(data.items);
                self.companyList.manualMaxIndex(Math.ceil(data.totalCount / self.companyList.pageSize) - 1);
            }
        });
    }

    self.AddCompany = function () {

        if (self.itemToAdd() != "") {

            $.ajax({
                type: "POST",
                dataType: "json",
                url: webapiUrl + 'api/company',
                contentType: 'application/json',
                data: JSON.stringify({ name: self.itemToAdd() }),
                success: function (data) {

                    self.GetCompanies();
                    self.itemToAdd("");
                }
            });


        }
    }.bind(this);

    var UpdateCompany = function (itemToUpdate) {

        $.ajax({
            type: "PUT",
            dataType: "json",
            url: webapiUrl + 'api/company',
            contentType: 'application/json',
            data: JSON.stringify(itemToUpdate),
            success: function (data) {

            }
        });
    };

    var DeleteCompany = function (itemId) {

        $.ajax({
            type: "DELETE",
            dataType: "json",
            url: webapiUrl + 'api/company/' + itemId,
            success: function (data) {

                self.GetCompanies();
            }
        });
    };

    self.companyList = new ko.simpleGrid.viewModel({
        data: self.items,
        columns: [
            { headerText: "Name", rowText: "name" },
            {
                headerText: "", rowText: {
                    edit: function (item) {
                        return function () {

                            $(".main-list input").attr("readonly", true);
                            $(".main-list input").addClass("input-readonly");

                            var $inputToEdit = $(".main-list input[data-id=" + item.id + "]");
                            $inputToEdit.attr("readonly", false);
                            $inputToEdit.removeClass("input-readonly");

                            $inputToEdit.on('blur', function () {
                                if (item.name) {
                                    $inputToEdit.attr("readonly", true);
                                    $inputToEdit.addClass("input-readonly");
                                    UpdateCompany(item);
                                    $(".validation-message").hide();
                                }
                                else {
                                    $(".validation-message").show();
                                }
                            });
                        }
                    }
                }
            },
            {
                headerText: "", rowText: {
                    delete: function (item) {
                        return function () {

                            DeleteCompany(item.id);
                            self.items.remove(item);
                        }
                    }
                }
            },
            {
                headerText: "", rowText: {
                    view: function (item) {
                        return function () {
                            location.href = '/employee/list?companyid=' + item.id + '&companyname=' + item.name;
                        }
                    }
                }
            }
        ],
        pageSize: 10,
        requestFunction: self.GetCompanies
    });

    self.GetCompanies();
};

ko.applyBindings(new CompanyListViewModel());