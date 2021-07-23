class Dropdown {
  constructor(dropdown) {
    this.Dropdown = dropdown;
    this.loadDropdown();
  }

  loadDropdown() {
    this.changeMenu();
    this.renderDropdown();
  }

  renderMenu(data) {
    let dropdown = this.Dropdown;
    let menu = dropdown.children[2];
    let tmp = "";
    let itemSelected = $(dropdown).attr("value") || null;

    $(menu).empty();

    data.forEach((item) => {
      tmp = $(`<div id="${item.id}" class="dropdown-item" ${
        item.id === itemSelected ? "checked" : ""
      }>
              <div class="item-icon">
                <i class="icon-16 fas fa-check"></i>
              </div>
              <div class="item-label">${item.label}</div>
            </div>`);

      if (item.id === itemSelected) $(dropdown.children[0]).html(item.label);

      $(tmp).click((e) => {
        let input = dropdown.children[0];
        $(input).html(item.label);
        $(dropdown).attr("value", item.id);
      });

      $(menu).append(tmp);
    });
  }

  renderDropdown() {
    let dropdown = this.Dropdown;
    let key = $(dropdown).attr("key");
    let tmp = [];

    if (key === "department") {
      getDepartment().done((res) => {
        res.forEach(({ DepartmentId, DepartmentName }) =>
          tmp.push({
            id: DepartmentId,
            label: DepartmentName,
          })
        );

        this.renderMenu(tmp);
      });
    } else if (key === "position") {
      getPosition().done((res) => {
        res.forEach(({ PositionId, PositionName }) =>
          tmp.push({
            id: PositionId,
            label: PositionName,
          })
        );

        this.renderMenu(tmp);
      });
    } else this.renderMenu(formData[dropdown.id]);
  }

  changeMenu() {
    let dropdown = this.Dropdown;
    let toggle = dropdown.children[1].children[0];
    let toggleIcon = toggle.classList;
    let menu = dropdown.children[2];

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
