const formData = {
  Gender: [
    { id: "0", label: "Nữ" },
    { id: "1", label: "Nam" },
    { id: "2", label: "Không xác định" },
  ],
  WorkStatus: [
    { id: "0", label: "Thất nghiệp" },
    { id: "1", label: "Đang làm việc" },
  ],
};

let form = $("div.form-employee");

let btnClose = $("div.form-employee div.btn-close");
let btnSave = $("div.form-employee div.form-footer div.btn-save");
let btnCancel = $("div.form-employee div.form-footer div.btn-cancel");
let dropdownForm = $("div.item div.dropdown");

dropdownForm.click((e) => {
  new Dropdown(e.currentTarget).handleDropdown();
});

function openModal() {
  modal.show();
  form.show();
}

function closeModal() {
  modal.hide();
  form.hide();
}

// clear form before display form
function clearForm() {
  $("div.item input").val(" ");
  $("div.item input").removeClass("input-required");
  $("div.item span.tooltiptext").removeClass("tooltiptext-active");
  $("div.item div.dropdown div.dropdown-input").html("");
}

btnClose.click(() => {
  closeModal();
});

btnCancel.click(() => {
  closeModal();
});

btnSave.click(() => {
  createNewEmployee($("div.form-employee").attr("id"));
});

$("input[required]").each(function () {
  let input = new Input(this, "test1", "text", "mony");
  input.blurInput();
  input.clearTooltip();
});

/*
  if formmode = 1 ---> create new employee
  else formode = 0 ----> change new employee  vs id 
*/

function createNewEmployee(id = null) {
  let tmp = dataSchema;

  formProp.forEach((item) => {
    let prop = $(`div.item #${item.id}`);

    if (prop.attr("type") === "date") {
      tmp[item.id] = prop.val();
    } else if (prop.attr("type") === "text") {
      tmp[item.id] = prop.val();
    } else {
      tmp[item.value || item.id] = prop.attr("value");
    }
  });

  if (formMode === 1) {
    newEmployee(tmp)
      .done((res) => {
        alert("Done");
        closeModal();
        loadData();
      })
      .fail((err) => {
        alert("Fail");
        loadData();
        closeModal();
      });
  } else {
    changeEmployee(id, tmp)
      .done((res) => {
        alert("Done");
        closeModal();
        loadData();
      })
      .fail((err) => {
        alert("Fail");
        loadData();
        closeModal();
      });
  }
}

const dataSchema = {
  CreatedDate: "2021-07-21T08:58:26.162Z",
  CreatedBy: "John Wick",
  ModifiedDate: "2021-07-21T08:58:26.162Z",
  ModifiedBy: "John Wick",
  EmployeeId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  EmployeeCode: "string",
  FirstName: "Wick",
  LastName: "John",
  FullName: "string",
  Gender: 0,
  DateOfBirth: "2021-07-21T08:58:26.162Z",
  PhoneNumber: "string",
  Email: "string",
  Address: "Ha Noi",
  IdentityNumber: "string",
  IdentityDate: "2021-07-21T08:58:26.162Z",
  IdentityPlace: "string",
  JoinDate: "2021-07-21T08:58:26.162Z",
  MartialStatus: 0,
  EducationalBackground: 0,
  QualificationId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  DepartmentId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  PositionId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  WorkStatus: 0,
  PersonalTaxCode: "string",
  Salary: 0,
  PositionCode: null,
  PositionName: null,
  DepartmentCode: null,
  DepartmentName: null,
  QualificationName: "John Wick",
};
