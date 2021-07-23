class Form {
  static dataSchema = {
    CreatedDate: "2021-07-21T08:58:26.162Z",
    CreatedBy: "John Wick",
    ModifiedDate: "2021-07-21T08:58:26.162Z",
    ModifiedBy: "John Wick",
    EmployeeId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    EmployeeCode: "string",
    FirstName: "Wick",
    LastName: "John",
    FullName: "string",
    Gender: 0,
    DateOfBirth: "2021-07-21T08:58:26.162Z",
    PhoneNumber: "string",
    Email: "string",
    Address: "Ha Noi",
    IdentityNumber: "string",
    IdentityDate: "2021-07-21T08:58:26.162Z",
    IdentityPlace: "string",
    JoinDate: "2021-07-21T08:58:26.162Z",
    MartialStatus: 0,
    EducationalBackground: 0,
    QualificationId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    DepartmentId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    PositionId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    WorkStatus: 0,
    PersonalTaxCode: "string",
    Salary: 0,
    PositionCode: null,
    PositionName: null,
    DepartmentCode: null,
    DepartmentName: null,
    QualificationName: "John Wick",
  };

  static modal = $("div.modal");

  constructor(form, id, formMode = 1, data = null) {
    this.Form = $(form);
    this.FormMode = formMode;
    this.Id = id;
    this.BtnClose = $(`${form} div.btn-close`);
    this.BtnSave = $(`${form} div.form-footer div.btn-save`);
    this.BtnCancel = $(`${form} div.form-footer div.btn-cancel`);
    this.Dropdown = $("div.item div.dropdown");

    this.loadData(data);
    this.initEvent(data);
  }

  initEvent() {
    this.BtnClose.click(() => {
      this.closeForm();
    });

    this.BtnCancel.click(() => {
      this.closeForm();
    });
  }

  loadData(data) {
    this.clearForm();
    this.initInput(data);
    this.initDropdown(data);
    this.openForm();
  }

  initInput(data) {
    $("div.item input").each(function (index, item) {
      let input = new Input(
        this,
        item.id,
        $(item).attr("dataType"),
        item.required
      );
      input.Value = data ? data[item.id] : null;
    });
  }

  initDropdown(data) {
    if (data)
      this.Dropdown.each(function (index, item) {
        $(item).attr("value", data[item.id]);
      });

    this.Dropdown.click((e) => {
      new Dropdown(e.currentTarget);
    });
  }

  openForm() {
    this.Form.show();
    Form.modal.show();
  }

  closeForm() {
    this.Form.hide();
    Form.modal.hide();
  }

  clearForm() {
    $("div.item input").val(" ");
    $("div.item input").removeClass("input-required");
    $("div.item span.tooltiptext").removeClass("tooltiptext-active");
    $("div.item div.dropdown div.dropdown-input").html("");
  }

  handleSave() {}
}
