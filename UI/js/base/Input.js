class Input {
  constructor(
    input,
    id,
    format,
    valueDefault = null,
    required = false,
    placeHolder = null
  ) {
    this.Input = input;
    this.Id = id;
    this.Format = format;
    this.Required = required;
    this.ValueDefault = valueDefault;
    this.PlaceHolder = placeHolder;

    this.loadData();
    this.clearTooltip();
    this.blurInput();
    this.onChangeInput();
  }

  get Value() {
    return $(this.Input).val();
  }

  set Value(value) {
    $(this.Input).val(value);
  }

  loadData() {
    this.Value = this.ValueDefault;

    if (this.Id === "EmployeeCode") {
      $(this.Input).focus();
    }
  }

  /*
      Validate giá trị input khi blur input
      error -> thêm tooltip
  */

  blurInput() {
    let self = this;
    let input = self.Input;

    $(input).blur((e) => {
      let { isPassed, errorMessage } = self.validate();
      let tooltip = $(input).siblings("span.tooltiptext");

      if (!isPassed) {
        self.showTooltip(errorMessage);
      } else {
        $(input).removeClass("input-required");
        tooltip.removeClass("tooltiptext-active");
      }
    });
  }

  /* Hien thi tooltip khi mot truong bat buoc khong duoc nhap */

  showTooltip(error = "Nhân viên không được để trống!") {
    let tooltip = $(this.Input).siblings("span.tooltiptext");

    $(this.Input).addClass("input-required");
    $(tooltip).html(error);
    $(tooltip).addClass("tooltiptext-active");
  }

  /*
      Xử lý value input khi thay đổi
  */

  onChangeInput() {
    let self = this;
    $(this.Input).on("input", function (e) {
      self.formatValue(self, self.Value);
    });
  }

  /*
      Định giá value input
  */

  formatValue(self, value) {
    if (self.Format === "money") {
      let tmp = value.replaceAll(".", "");
      tmp = tmp.replaceAll(" ", "");
      this.Value = Common.formatMoney(tmp);
    }
  }

  /*
      Xóa tooltip
  */

  clearTooltip() {
    let input = this.Input;
    $(input).focus((e) => {
      let tooltip = $(input).siblings("span.tooltiptext");
      $(input).removeClass("input-required");
      tooltip.removeClass("tooltiptext-active");
    });
  }

  /*
      Validate giá trị input
  */

  validate() {
    let validate = { isPassed: true, errorMessage: null };

    if (this.Required) {
      if (this.Value === " " || this.Value === "" || !this.Value) {
        validate.isPassed = false;
        validate.errorMessage = "Thông tin bắt buộc!";
        return validate;
      }

      if (this.Format === "email") {
        validate.isPassed = Common.validateEmail(this.Value);
        validate.errorMessage = "Email không đúng định dạng";
        return validate;
      }
    }

    return validate;
  }
}
