class Popup {
  constructor(popup, func) {
    this.Popup = popup;
    this.Modal = "div.modal";
    this.Func = func;

    this.openPopup();
    this.initEvent();
  }

  initEvent() {
    $(`${this.Popup} div.btn-close`).click((e) => {
      this.closePopup();
    });

    $(`${this.Popup} div.popup-footer div.btn-left`).click((e) => {
      this.closePopup();
    });

    $(`${this.Popup} div.popup-footer div.btn-right`).click((e) => {
      try {
        this.Func();
        alert("done");
        this.closePopup();
      } catch (error) {
        if (error.status >= 500) {
          alert("Liên hệ Misa để biết thêm chi tiết");
          this.closeForm();
        } else {
          alert("Vui lòng khách hàng xem các thông tin điền");
        }
      }
    });
  }

  closePopup() {
    $(this.Popup).hide();
    $(this.Modal).hide();
  }

  openPopup() {
    $(this.Popup).show();
    $(this.Modal).show();
  }
}
