let popup = $("div.pop-up");
let modal = $("div.modal");

function showPopup() {
  popup.show();
  modal.show();
}

function closePopup() {
  popup.hide();
  modal.hide();
}

$("div.pop-up div.btn-close").click((e) => {
  closePopup();
});

$("div.pop-up div.popup-footer div.btn-left").click((e) => {
  closePopup();
});

$("div.pop-up div.popup-footer div.btn-right").click((e) => {
  let id = popup.attr("id");
  deleteEmployee(id)
    .done((res) => {
      alert("done");
      closePopup();
      loadData();
    })
    .fail((err) => {
      alert("fail");
      closePopup();
    });
});
