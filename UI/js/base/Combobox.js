function renderCombobox(combobox, data) {
  let menu = combobox.children[2];
  let input = combobox.children[0];
  let tmp = "";
  let tmpData = data;

  if (!tmpData) return;

  tmpData.forEach((item) => {
    tmp += `<div id="${item.id}" class="combo-box-item ${
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

function handleCombobox(combobox, data) {
  let input = combobox.children[0];
  let toggle = combobox.children[1];
  renderCombobox(combobox, data);

  let menu = combobox.children[2];

  handleMenuCombobox(combobox, data);
  clearInput(combobox);
  onChangeInput(combobox, data);

  toggle.addEventListener("click", (e) => {
    let toggleIcon = toggle.children[0].classList;

    if (toggleIcon.contains("fa-chevron-down")) {
      toggleIcon.remove("fa-chevron-down");
      toggleIcon.add("fa-chevron-up");
      menu.classList.remove("hide");
      combobox.classList.add("combobox-active");
      toggle.classList.add("combobox-icon-active");
      input.classList.add("combobox-input-active");
    } else {
      toggleIcon.add("fa-chevron-down");
      toggleIcon.remove("fa-chevron-up");
      menu.classList.add("hide");
      combobox.classList.remove("combobox-active");
      toggle.classList.remove("combobox-icon-active");
      input.classList.remove("combobox-input-active");
    }
  });
}

function handleMenuCombobox(combobox, data) {
  let input = combobox.children[0];
  let toggle = combobox.children[1];
  let menu = combobox.children[2];
  let done = combobox.children[3];
  let toggleIcon = toggle.children[0].classList;

  Array.from(menu.children).forEach((item) => {
    item.addEventListener("click", (e) => {
      let id;
      if (e.path[0].id) {
        id = e.path[0].id;
      } else {
        id = e.path[1].id;
      }

      data.forEach((item) => {
        if (item.id === parseInt(id)) {
          item.checked = true;

          combobox.children[0].children[0].value = item.label;
          menu.children[item.id - 1].classList.add("checked");
        } else {
          item.checked = false;
          menu.children[item.id - 1].classList.remove("checked");
        }
      });

      if (!toggleIcon.contains("fa-chevron-down")) {
        combobox.style.borderColor = "#bbb";
        toggleIcon.add("fa-chevron-down");
        toggleIcon.remove("fa-chevron-up");
        menu.classList.add("hide");
        combobox.classList.remove("combobox-active");
        toggle.classList.remove("combobox-icon-active");
        input.classList.remove("combobox-input-active");
        done.classList.add("combobox-done-active");
      }
    });
  });
}

const combobox = document.querySelectorAll("div.combo-box");
handleCombobox(combobox[0], [
  { id: 1, label: "Phòng Nhân Sự", checked: false },
  { id: 2, label: "Phòng Kỹ Thuật", checked: false },
  { id: 3, label: "Phòng Sale", checked: false },
  { id: 4, label: "Phòng Marketing", checked: false },
]);

window.onclick = (event) => {
  combobox.forEach((combobox) => defectClickOutside(combobox, event.target));
};
