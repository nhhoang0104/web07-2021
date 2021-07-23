/*
function renderMenu(dropdown, data) {
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

function renderDropdown(dropdown) {
  let tmp = [];

  if (dropdown.id === "department") {
    getDepartment().done((res) => {
      res.forEach(({ DepartmentId, DepartmentName }) =>
        tmp.push({
          id: DepartmentId,
          label: DepartmentName,
        })
      );

      renderMenu(dropdown, tmp);
    });
  } else if (dropdown.id === "position") {
    getPosition().done((res) => {
      res.forEach(({ PositionId, PositionName }) =>
        tmp.push({
          id: PositionId,
          label: PositionName,
        })
      );

      renderMenu(dropdown, tmp);
    });
  } else renderMenu(dropdown, formData[dropdown.id]);
}

function handleDropdown(dropdown) {
  renderDropdown(dropdown);
  changeMenu(dropdown);
}

function changeMenu(dropdown) {
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

function defectClickOutside(dropdown, positionCurr) {
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
*/
let dropdown = document.querySelectorAll("div.dropdown");

window.onclick = (event) => {
  dropdown.forEach((dropdown) => defectClickOutside(dropdown, event.target));
};
