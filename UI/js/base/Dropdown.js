class Dropdown {
  constructor(dropdown, itemSelected = null) {
    this.Dropdown = dropdown;
    this.Key = $(dropdown).attr("key");
    this.ClassName = $(dropdown).attr("className");
    this.Id = $(dropdown).attr("id");
    this.Label = $(dropdown).html();
    this.ItemSelected = itemSelected;
    this.Data = null;

    this.loadData();
    this.renderDropdown();
    this.initEvent();
  }

  /*
      * Khởi tạo sự kiện:
          - Sự kiện dropdown menu hiện, ân
  */

  initEvent() {
    $(this.Dropdown).on("click", (e) => {
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

  renderDropdown() {
    let dropdown = this.Dropdown;
    let dropdownHtml = "";

    dropdownHtml =
      $(`<div key=${this.Key}" id=${this.Id} class="${this.ClassName}" value="${this.ItemSelected}">
              <div class="dropdown-input">${this.Label}</div>
              <div class="dropdown-toggle">
                <i class="fas fa-chevron-down icon-16"></i>
              </div>
              <div class="dropdown-menu"></div>
            </div>`);

    $(dropdown).replaceWith(dropdownHtml);
    this.Dropdown = dropdownHtml;
  }

  changeMenu() {
    let dropdown = $(this.Dropdown);
    let toggle = dropdown.children()[1].children[0];
    let toggleIcon = toggle.classList;
    let menu = dropdown.children()[2];

    if (toggleIcon.contains("fa-chevron-down")) {
      $(dropdown).css("borderColor", "#019160");
      toggleIcon.remove("fa-chevron-down");
      toggleIcon.add("fa-chevron-up");
      $(menu).show();
    } else {
      $(dropdown).css("borderColor", "#bbb");
      toggleIcon.add("fa-chevron-down");
      toggleIcon.remove("fa-chevron-up");
      $(menu).hide();
    }
  }

  renderMenu() {
    let dropdown = $(this.Dropdown);
    let input = dropdown.children()[0];
    let menu = dropdown.children()[2];
    let tmp = "";

    //let itemSelected = this.ItemSelected;
    let itemSelected = $(dropdown).attr("value") || null;

    $(menu).empty();

    this.Data.forEach((item) => {
      tmp = $(`<div id="${item.id}" class="dropdown-item" ${
        item.id === itemSelected ? "checked" : ""
      }>
              <div class="item-icon">
                <i class="icon-16 fas fa-check"></i>
              </div>
              <div class="item-label">${item.label}</div>
            </div>`);

      if (item.id === itemSelected) $(input).html(item.label);

      $(tmp).click((e) => {
        this.ItemSelected = item.id;
        $(input).html(item.label);
        $(dropdown).attr("value", item.id);
      });

      $(menu).append(tmp);
    });
  }

  static defectClickOutside(dropdown, positionCurr) {
    let toggle = dropdown.children[1].children[0];
    let toggleIcon = toggle.classList;
    let menu = dropdown.children[2];

    if (!dropdown.contains(positionCurr)) {
      if (!toggleIcon.contains("fa-chevron-down")) {
        $(dropdown).css("borderColor", "#bbb");
        toggleIcon.add("fa-chevron-down");
        toggleIcon.remove("fa-chevron-up");
        $(menu).hide();
      }
    }
  }
}
