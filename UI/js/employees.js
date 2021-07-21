var formMode = 1;
const formProp = [
  { id: "EmployeeCode" },
  { id: "FullName" },
  { id: "DateOfBirth" },
  { id: "Gender" },
  { id: "IdentityNumber" },
  { id: "IdentityDate" },
  { id: "IdentityPlace" },
  { id: "Email" },
  { id: "PhoneNumber" },
  { id: "position", value: "PositionId", label: "PositionName" },
  { id: "department", value: "DepartmentId", label: "DepartmentName" },
  { id: "PersonalTaxCode" },
  { id: "Salary" },
  { id: "JoinDate" },
  { id: "WorkStatus" },
];

function loadData() {
  let table = $("div.list > table > tbody");

  table.empty();

  getAllEmployees()
    .done((res) => {
      res.forEach((item) => {
        const tmpHtml = `<tr id=${item.EmployeeId}>
                <td class="text-align-left">${item.EmployeeCode || ""}</td>
                <td class="text-align-left">${item.FullName || ""}</td>
                <td class="text-align-center">${formatDate(
                  item.DateOfBirth
                )}</td>
                <td class="text-align-left">${item.GenderName || ""}</td>
                <td class="text-align-left">${item.PhoneNumber || ""}</td>
                <td class="text-align-left">${item.Email || ""}</td>
                <td class="text-align-left">${item.PositionName || ""}</td>
                <td class="text-align-left">${item.DepartmentName || ""}</td>
                <td class="text-align-right">${
                  item.Salary ? formatMoney(item.Salary) : ""
                }</td>
                <td class="text-align-left">${item.WorkStatus || ""}</td>
            </tr>`;
        table.append(tmpHtml);
      });
    })
    .fail((err) => {});
}

// Refresh data
function refreshData() {
  let btnRefresh = $("div.toolbar-right button");

  btnRefresh.click((e) => loadData());
}

// handle click employee => display information. can change
function handleEmployee() {
  $("div.list table").on("click", "tbody tr", (e) => {
    formMode = 0;
    const id = e.currentTarget.id;

    getEmployeeById(id).done((res) => {
      formProp.forEach((item) => {
        let prop = $(`div.item #${item.id}`);

        if (prop.attr("type") === "date") {
          prop.val(formatDateInput(res[item.id]));
        } else if (prop.attr("type") === "text") {
          prop.val(res[item.id]);
        } else {
          if (item.value) {
            prop.attr("value", res[item.value]);
          } else {
            prop.attr("value", res[item.id]);
          }
        }
      });

      openModal();
    });
  });
}

loadData();

refreshData();

handleEmployee();

// handle click toolbar
let dropdownToolbar = $("div.toolbar-left div.dropdown");
dropdownToolbar.click((e) => {
  handleDropdown(e.currentTarget);
});
