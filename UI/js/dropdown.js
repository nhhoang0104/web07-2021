let listOfDropdown = document.querySelectorAll("div.dropdown");

let data = {
  division: [
    { id: 1, label: "Phòng Nhân Sự", checked: false },
    { id: 2, label: "Phòng Kỹ Thuật", checked: false },
    { id: 3, label: "Phòng Sale", checked: false },
    { id: 4, label: "Phòng Marketing", checked: false },
  ],
  position: [
    { id: 1, label: "Giám đôcs", checked: false },
    { id: 2, label: "Phó Giám đốc", checked: false },
    { id: 3, label: "Trưởng phòng", checked: false },
    { id: 4, label: "Nhân viên Sale", checked: false },
  ],
};

function renderDropdown(dropdown) {
  let menu = dropdown.children[2];
  let input = dropdown.children[0];
  let tmp = "";
  let tmpData = data[dropdown.id];

  if (!tmpData) return;

  tmpData.forEach((item) => {
    tmp += `<div id="${item.id}" class="dropdown-item ${
      item.checked ? "checked" : ""
    }">
                  <div class="item-icon">
                    <i class="icon-16 fas fa-check"></i>
                  </div>
                <div class="item-label">${item.label}</div>
              </div>`;
    if (item.checked) input.innerHTML = item.label;
  });

  menu.innerHTML = tmp;
}

function handleDropdown(dropdown) {
  renderDropdown(dropdown);
  let menu = dropdown.children[2];

  Array.from(menu.children).forEach((item) => {
    item.addEventListener("click", (e) => {
      if (e.path.length === 11) {
        id = e.path[0].id;
      } else if (e.path.length === 12) {
        id = e.path[1].id;
      }

      data[dropdown.id].forEach((item) => {
        if (item.id === parseInt(id)) {
          item.checked = true;
          dropdown.children[0].innerHTML = item.label;
          menu.children[item.id - 1].classList.add("checked");
        } else {
          item.checked = false;
          menu.children[item.id - 1].classList.remove("checked");
        }
      });
    });
  });

  dropdown.addEventListener("click", () => {
    let toggle = dropdown.children[1].children[0];
    let toggleIcon = toggle.classList;

    if (toggleIcon.contains("fa-chevron-down")) {
      dropdown.style.borderColor = "#019160";
      toggleIcon.remove("fa-chevron-down");
      toggleIcon.add("fa-chevron-up");
      menu.classList.remove("hide");
    } else {
      dropdown.style.borderColor = "#bbb";
      toggleIcon.add("fa-chevron-down");
      toggleIcon.remove("fa-chevron-up");
      menu.classList.add("hide");
    }
  });
}

function handleMenu(dropdown) {
  console.log(dropdown);
  let menu = dropdown.children[2];
}

function defectClickDropdown(dropdown, positionCurr) {
  if (!dropdown.contains(positionCurr)) {
    let toggle = dropdown.children[1].children[0];

    if (!toggle.classList.contains("fa-chevron-down")) {
      let dropdownData = dropdown.children[2];
      toggle.classList.add("fa-chevron-down");
      toggle.classList.remove("fa-chevron-up");
      dropdownData.classList.add("hide");
    }
  }
}

listOfDropdown.forEach((dropdown) => {
  handleDropdown(dropdown);
});
