class Combobox {
  constructor(combobox, itemSelected = null, required = false) {
    this.Combobox = combobox;
    this.Key = $(combobox).attr("key");
    this.ClassName = $(combobox).attr("className");
    this.Id = $(combobox).attr("id");
    this.Label = $(combobox).html();
    this.ItemSelected = itemSelected;
    this.Required = required;
    this.Data = null;

    this.loadData();
    this.renderCombobox();
    this.initEvent();
  }

  /*
      * Khởi tạo sự kiện:
          - Sự kiện dropdown menu hiện, ân
  */

  initEvent() {
    let toggle = this.Combobox.children()[1];
    $(toggle).on("click", (e) => {
      this.changeMenu();
      this.renderMenu();
    });
  }

  /* load data cho dropdown menu*/

  loadData() {
    let key = this.Key;
    let tmp = [];

    if (key === "department") {
      getDepartment().done((res) => {
        res.forEach(({ DepartmentId, DepartmentName }) =>
          tmp.push({
            id: DepartmentId,
            label: DepartmentName,
          })
        );

        this.Data = tmp;
      });
    } else if (key === "position") {
      getPosition().done((res) => {
        res.forEach(({ PositionId, PositionName }) =>
          tmp.push({
            id: PositionId,
            label: PositionName,
          })
        );

        this.Data = tmp;
      });
    } else this.Data = formData[key];
  }

  /*  render dropdown */

  renderCombobox() {
    let combobox = this.Combobox;
    let comboboxHtml = "";

    comboboxHtml =
      $(`<div key=${this.Key}" id=${this.Id} class="${this.ClassName}" value="${this.ItemSelected}">
              <div class="combo-box-input"><input type="text" placeholder="Nhap thong tin" /></div>
              <div class="combo-box-icon">
                <i class="fas fa-chevron-down icon-16"></i>
              </div>
              <div class="combo-box-menu"></div>
              <div class="combo-box-done"><i class="fas fa-times icon-10"></i></div>
            </div>`);

    $(combobox).replaceWith(comboboxHtml);

    this.Combobox = comboboxHtml;
  }

  /* ẩn hiện menu */

  changeMenu() {
    let combobox = $(this.Combobox);
    let input = combobox.children()[0];
    let toggle = combobox.children()[1].children[0];
    let toggleIcon = toggle.classList;
    let menu = combobox.children()[2];

    if (toggleIcon.contains("fa-chevron-down")) {
      $(combobox).css("borderColor", "#019160");
      toggleIcon.remove("fa-chevron-down");
      toggleIcon.add("fa-chevron-up");
      $(menu).show();
      combobox.add("combobox-active");
      $(toggle).add("combobox-icon-active");
      $(input).add("combobox-input-active");
    } else {
      $(combobox).css("borderColor", "#bbb");
      toggleIcon.add("fa-chevron-down");
      toggleIcon.remove("fa-chevron-up");
      $(menu).hide();
      combobox.remove("combobox-active");
      $(toggle).remove("combobox-icon-active");
      $(input).remove("combobox-input-active");
    }
  }

  /* render menu */

  renderMenu() {
    let combobox = $(this.Combobox);
    let input = combobox.children()[0];
    let menu = combobox.children()[2];
    let tmp = "";

    //let itemSelected = this.ItemSelected;
    let itemSelected = $(combobox).attr("value") || null;

    $(menu).empty();

    this.Data.forEach((item) => {
      tmp = $(`<div id="${item.id}" class="combo-box-item" ${
        item.id === itemSelected ? "checked" : ""
      }>
              <div class="item-icon">
                <i class="icon-16 fas fa-check"></i>
              </div>
              <div class="item-label">${item.label}</div>
            </div>`);

      if (item.id === itemSelected) $(input).val(item.label);

      $(tmp).click((e) => {
        this.ItemSelected = item.id;
        $(input).val(item.label);
        $(combobox).attr("value", item.id);
      });

      $(menu).append(tmp);
    });
  }
}
/*
function onChangeInput(combobox, data) {
  let input = combobox.children[0];
  let toggle = combobox.children[1];
  let menu = combobox.children[2];

  let toggleIcon = toggle.children[0].classList;

  input.addEventListener("change", (e) => {
    search(e.target.value, data);

    if (toggleIcon.contains("fa-chevron-down")) {
      toggleIcon.remove("fa-chevron-down");
      toggleIcon.add("fa-chevron-up");
      menu.classList.remove("hide");
      combobox.classList.add("combobox-active");
      toggle.classList.add("combobox-icon-active");
      input.classList.add("combobox-input-active");
    }
  });
}

function clearInput(combobox) {
  let iconDone = combobox.children[3];
  let input = combobox.children[0].children[0];

  iconDone.addEventListener("click", () => {
    iconDone.classList.remove("combobox-done-active");
    input.value = "";
  });
}

*/
