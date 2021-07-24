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

const tableProp = [
  { label: "Mã nhân viên", id: "EmployeeCode", className: "text-align-left" },
  { label: "Họ và tên", id: "Fullname", className: "text-align-left" },
  { label: "Ngày sinh", id: "DateOfBirth", className: "text-align-center" },
  { label: "Giới tính", id: "GenderName", className: "text-align-left" },
  { label: "Điện thoại", id: "PhoneNumber", className: "text-align-left" },
  { label: "Email", id: "Email", className: "text-align-left" },
  { label: "Chức vụ", id: "PositionName", className: "text-align-left" },
  { label: "Phòng ban", id: "DepartmentName", className: "text-align-left" },
  { label: "Mức lức cơ bản", id: "Salary", className: "text-align-right" },
  {
    label: "Tình trạng công việc",
    id: "WorkStatus",
    className: "text-align-left",
  },
];

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

var formMode = 1;

let table = new Table("div.list", tableProp, getAllEmployees, deleteEmployee);

// Refresh data
function eventRefreshData() {
  let btnRefresh = $("div.toolbar-right button");

  btnRefresh.click((e) => table.loadData());
}

eventRefreshData();

// event add new employee
$("#add-new-employee").click((e) => {
  formMode = 1;
  new Form("div.form-employee");
});

// event click dropdown toolbar
let dropdownToolbar = $("div.toolbar-left dropdown");

dropdownToolbar.each((index, item) => {
  new Dropdown(item);
});

// event click outside dropdown
let dropdown = document.querySelectorAll("div.dropdown");

window.onclick = (event) => {
  dropdown.forEach((dropdown) =>
    Dropdown.defectClickOutside(dropdown, event.target)
  );
};
