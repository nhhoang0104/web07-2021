function getAllEmployees() {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Employees",
    method: "GET",
  });
}

function getNewEmployeeCode() {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode",
    method: "GET",
  });
}

function getEmployeeById(id) {
  return $.ajax({
    url: `http://cukcuk.manhnv.net/v1/Employees/${id}`,
    method: "GET",
  });
}

function newEmployee(data) {
  return $.ajax({
    url: "http://cukcuk.manhnv.net/v1/Employees",
    method: "POST",
    data: JSON.stringify(data),
    dataType: "json",
    contentType: "application/json",
  });
}

function changeEmployee(id, data) {
  return $.ajax({
    url: `http://cukcuk.manhnv.net/v1/Employees/${id}`,
    method: "PUT",
    data: JSON.stringify(data),
    dataType: "json",
    contentType: "application/json",
  });
}
