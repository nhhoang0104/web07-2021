class Input {
  constructor(input, id, format, required = false) {
    this.Input = input;
    this.Id = id;
    this.Format = format;
    this.Required = required;

    this.clearTooltip();
    this.blurInput();
    this.onChangeInput();
  }

  get Value() {
    return $(this).value;
  }

  set Value(value) {
    $(this.Input).val(value);
  }

  /*
      Validate giá trị input khi blur input
      error -> thêm tooltip
  */

  blurInput() {
    if (this.Required) {
      let input = this.Input;
      $(input).blur(() => {
        let tooltip = $(input).siblings("span.tooltiptext");

        if ($(input).val() === " " || $(input).val() === "") {
          $(input).addClass("input-required");
          tooltip.html("Nhân viên không được để trống!");
          tooltip.addClass("tooltiptext-active");
        } else {
          $(input).removeClass("input-required");
          tooltip.removeClass("tooltiptext-active");
        }
      });
    }
  }

  /*
      Xử lý value input khi thay đổi
  */

  onChangeInput() {
    let self = this;
    $(this.Input).on("input", function (e) {
      self.formatValue(self, $(this).val());
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

  validate() {}
}
