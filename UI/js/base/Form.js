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
    this.DropdownList = [];
    this.InputList = [];
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

  /*
    Khởi tạo các input có trong form, set giá trị mặc định nếu cố.
  */

  initInput(data) {
    let self = this;
    $("div.item input").each(function (index, item) {
      self.InputList.push(
        new Input(
          this,
          item.id,
          $(item).attr("dataType"),
          data ? data[item.id] : null,
          item.required
        )
      );
    });
  }

  /*
    Khởi tạo các dropdown có trong form, set giá trị mặc định nếu cố.
  */

  initDropdown(data) {
    let self = this;
    $("div.item dropdown").each(function (index, item) {
      self.DropdownList.push(new Dropdown(item, data ? data[item.id] : null));
    });
  }

  /* 
    Mở form
  */

  openForm() {
    $($(this.Form)).show();
    Form.modal.show();
  }

  /* 
    Đóng form
  */

  closeForm() {
    $(this.Form).hide();
    Form.modal.hide();
    let dropdown = this.DropdownList;

    $("div.item div.dropdown").each(function (index, item) {
      $(item).replaceWith(dropdown[index]);
    });
  }

  /*
    reset form trước khi mở
  */

  clearForm() {
    $("div.item input").val(" ");
    $("div.item input").removeClass("input-required");
    $("div.item span.tooltiptext").removeClass("tooltiptext-active");
  }

  /*
      Xử lý khi sự kiện btnSave: thay đổi dư liệu hoặc thêm mới dl
  */

  async handleSave() {
    if (!this.validateForm()) return;

    let dataSchema = Form.DataSchema;

    let props = $(
      `${this.Form} div.form-body div.form-body-right div.row div.item`
    );

    props.each(function (index, item) {
      let tmp = $($(item).children()[1]);

      let prop = tmp.attr("id");

      if ($(tmp).attr("required")) {
      }

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
      try {
        await newEmployee(dataSchema);
        alert("Done");
        this.closeForm();
      } catch (error) {
        if (error.status >= 500) {
          alert("Liên hệ Misa để biết thêm chi tiết");
          this.closeForm();
        } else {
          alert("Vui lòng khách hàng xem các thông tin điền");
        }
      }
    } else {
      try {
        await changeEmployee(this.Id, dataSchema);
        this.closeForm();
      } catch (error) {
        if (error.status >= 500) {
          alert("Liên hệ Misa để biết thêm chi tiết");
          this.closeForm();
        } else {
          alert("Vui lòng xem các thông tin điền");
        }
      }
    }
  }

  /*
      Validate các trường bắt buộc trước khi lưu
  */

  validateForm() {
    let isAuth = true;
    this.InputList.forEach((item) => {
      let { isPassed, errorMessage } = item.validate();
      if (!isPassed) {
        item.showTooltip(errorMessage);
        isAuth = false;
      }
    });

    return isAuth;
  }
}
