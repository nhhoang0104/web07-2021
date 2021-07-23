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
      this.Func()
        .done((res) => {
          alert("done");
          this.closePopup();
        })
        .fail((err) => {
          alert("fail");
          this.closePopup();
        });
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
