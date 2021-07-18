let newEmployee = document.querySelector("#add-new-employee");
let btnClose = document.querySelector("div.close");
let modal = document.querySelector("div.modal");
let form = document.querySelector("div.form-employee");

newEmployee.addEventListener("click", () => {
  modal.classList.add("show");
  form.classList.add("show");
});

btnClose.addEventListener("click", () => {
  modal.classList.remove("show");
  form.classList.remove("show");
});
