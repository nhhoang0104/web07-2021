class Popup {
  constructor(popup, header, content, func) {
    this.Popup = popup;
    this.Header = header;
    this.Content = content;

    this.Modal = "div.modal";
    this.Func = func;

    this.renderPopup();
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

  renderPopup() {
    let self = this;
    let tmpHtml = $(`<div class="pop-up">
      <div class="btn-close"><i class="fas fa-times"></i></div>
      <div class="popup-header">${self.Header}</div>
      <div class="popup-content">
        <div class="icon"><i class="fas fa-exclamation-triangle"></i></div>
        <div class="label">${self.Content}</div>
      </div>
      <div class="popup-footer">
        <div class="btn btn-left">Huỷ</div>
        <div class="btn btn-right">Xóa</div>
      </div>
    </div>`);

    $(this.Popup).replaceWith(tmpHtml);
    this.Popup = "div.pop-up";
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
