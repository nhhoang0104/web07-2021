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

//load data from api

function loadData() {
  let table = $("div.list > table > tbody");

  table.empty();

  getAllEmployees()
    .done((res) => {
      res.forEach((item) => {
        const tmpHtml = `<tr id=${item.EmployeeId} employeeCode=${
          item.EmployeeCode
        }
        }>
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
function eventRefreshData() {
  let btnRefresh = $("div.toolbar-right button");

  btnRefresh.click((e) => loadData());
}

// handle click employee => display information. can change
function eventViewInfoEmployee() {
  $("div.list table").on("dblclick", "tbody tr", (e) => {
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

      $("div.form-employee").attr("id", id);
      $("div.item input").removeClass("input-required");
      $("div.item span.tooltiptext").removeClass("tooltiptext-active");
      openModal();
    });
  });
}

//event click mouse right ---> delete employee
function eventDeleteEmployee() {
  $("div.list table").on("mousedown", "tbody tr", (e) => {
    if (e.which === 3) {
      $("div.pop-up div.popup-header").html("Xóa bản ghi");
      $("div.pop-up div.popup-content div.label").html(
        `Bạn có muốn chắc xóa <b>"Thông tin nhân viên củ nhân viên ${$(
          e.currentTarget
        ).attr("employeeCode")}"</b> hay không ?`
      );

      popup.attr("id", e.currentTarget.id);
      showPopup();
    }
  });
}

loadData();

eventRefreshData();

eventViewInfoEmployee();

eventDeleteEmployee();

// event add new employee
$("#add-new-employee").click((e) => {
  formMode = 1;

  clearForm();

  getNewEmployeeCode().done((res) => {
    $("div.item input[id=EmployeeCode]").val(res);
    $("div.item input[id=EmployeeCode]").focus();
  });

  openModal();
});

// vent click toolbar
let dropdownToolbar = $("div.toolbar-left div.dropdown");
dropdownToolbar.click((e) => {
  handleDropdown(e.currentTarget);
});
