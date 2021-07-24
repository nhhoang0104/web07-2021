class Form {
  static DataSchema = {
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
    this.Form = form;
    this.FormMode = formMode;

    this.Id = id;
    this.BtnClose = $(`${form} div.btn-close`);
    this.BtnSave = $(`${form} div.form-footer div.btn-save`);
    this.BtnCancel = $(`${form} div.form-footer div.btn-cancel`);
    this.Dropdown = $("div.item dropdown");

    this.loadData(data);
    this.initEvent();
  }

  initEvent() {
    let self = this;
    self.BtnClose.click(() => {
      self.closeForm();
    });

    self.BtnCancel.click(() => {
      self.closeForm();
    });

    self.BtnSave.click(() => {
      self.handleSave();
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
    this.Dropdown.each(function (index, item) {
      new Dropdown(item, data ? data[item.id] : null);
    });
  }

  openForm() {
    $($(this.Form)).show();
    Form.modal.show();
  }

  closeForm() {
    $(this.Form).hide();
    Form.modal.hide();
    let dropdown = this.Dropdown;

    $("div.item div.dropdown").each(function (index, item) {
      $(item).replaceWith(dropdown[index]);
    });
  }

  clearForm() {
    $("div.item input").val(" ");
    $("div.item input").removeClass("input-required");
    $("div.item span.tooltiptext").removeClass("tooltiptext-active");
  }

  handleSave() {
    let dataSchema = Form.DataSchema;

    let props = $(
      `${this.Form} div.form-body div.form-body-right div.row div.item`
    );

    props.each(function (index, item) {
      let tmp = $($(item).children()[1]);
      let prop = tmp.attr("id");

      if (tmp.attr("class") === "dropdown") {
        dataSchema[prop] = tmp.attr("value");
      }

      if (tmp.attr("class") && tmp.attr("class").includes("text-box")) {
        let dataType = tmp.attr("dataType");
        switch (dataType) {
          case "money":
            dataSchema[prop] = tmp.val().replace(".", "");
            break;
          case "date":
            dataSchema[prop] = Common.formatDateInput(tmp.val());
            break;
          default:
            dataSchema[prop] = tmp.val();
            break;
        }
      }
    });

    if (this.FormMode === 1) {
      newEmployee(dataSchema)
        .done((res) => {
          this.closeForm();
          alert("Done");
        })
        .fail((err) => {
          this.closeForm();
          alert("Fail");
        });
    } else {
      changeEmployee(this.Id, dataSchema)
        .done((res) => {
          this.closeForm();
          alert("Done");
        })
        .fail((err) => {
          this.closeForm();
          alert("Fail");
        });
    }
  }
}
