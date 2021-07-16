let listOfDropdown = document.querySelectorAll("div.dropdown");

const data = [
  { id: 1, label: "Test 1", checked: true },
  { id: 2, label: "Test 2", checked: false },
  { id: 3, label: "Test 3", checked: false },
  { id: 4, label: "Test 4", checked: false },
];

function renderDropdown(data) {
  let dropdown = document.querySelector("div.dropdown");
  let menu = dropdown.children[2];
  let input = dropdown.children[0];

  let tmp = "";

  data.forEach((item) => {
    tmp += `<div id="${item.id}" class="dropdown-item ${
      item.checked ? "checked" : ""
    }">
                  <div class="item-icon">
                    <i class="icon-16 fas fa-check"></i>
                  </div>
                <div class="label-item">${item.label}</div>
              </div>`;
    if (item.checked) input.innerHTML = item.label;
  });

  menu.innerHTML = tmp;
}

renderDropdown(data);

function handleDropdown(dropdown) {
  let dropdownMenu = dropdown.children[2];

  dropdown.addEventListener("click", () => {
    let toggle = dropdown.children[1].children[0];
    let toggleIcon = toggle.classList;

    if (toggleIcon.contains("fa-chevron-down")) {
      toggleIcon.remove("fa-chevron-down");
      toggleIcon.add("fa-chevron-up");
      dropdownMenu.classList.remove("hide");
      handleDropdownMenu(dropdownMenu);
    } else {
      toggleIcon.add("fa-chevron-down");
      toggleIcon.remove("fa-chevron-up");
      dropdownMenu.classList.add("hide");
    }
  });
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

function handleDropdownMenu(menu) {
  Array.from(menu.children).forEach((item) => {
    item.addEventListener(
      "click",
      (e) => {
        data.forEach((item) => {
          if (item.checked == true) {
            item.checked = false;
          }

          if (item.id == e.target.id) {
            item.checked = true;
          }
        });

        renderDropdown(data);
      },
      false
    );
  });
}

listOfDropdown.forEach((dropdown) => handleDropdown(dropdown));

window.onclick = (event) => {
  listOfDropdown.forEach((dropdown) =>
    defectClickDropdown(dropdown, event.target)
  );
};
