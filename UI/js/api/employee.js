// lấy dữ liệu về tất cả employee
function getAllEmployees() {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Employees",
    method: "GET",
  });
}

// lấy mã Employee Code mới

function getNewEmployeeCode() {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode",
    method: "GET",
  });
}

// Lấy info Employee bởi Id

function getEmployeeById(id) {
  return $.ajax({
    url: `http://cukcuk.manhnv.net/v1/Employees/${id}`,
    method: "GET",
  });
}

// Tạo mới nhân viên mới

function newEmployee(data) {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Employees",
    method: "POST",
    data: JSON.stringify(data),
    dataType: "json",
    contentType: "application/json",
  });
}

// Thay đổi thông tin nhân viên

function changeEmployee(id, data) {
  return $.ajax({
    url: `http://cukcuk.manhnv.net/v1/Employees/${id}`,
    method: "PUT",
    data: JSON.stringify(data),
    dataType: "json",
    contentType: "application/json",
  });
}

// Xóa nhân viên bởi id

function deleteEmployee(id) {
  return $.ajax({
    url: `http://cukcuk.manhnv.net/v1/Employees/${id}`,
    method: "DELETE",
    dataType: "json",
    contentType: "application/json",
  });
}
