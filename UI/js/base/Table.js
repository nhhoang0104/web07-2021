class Table {
  static formMode = 1;
  constructor(table, props, getData, removeItem) {
    this.Table = table;
    this.Props = props;
    this.GetData = getData;
    this.RemoveItem = removeItem;

    this.renderTable();
    this.loadData();
    this.initEvent();
  }

  initEvent() {
    // event double click for view detail info and change info
    $(`${this.Table} table`).on("dblclick", "tbody tr", (e) => {
      formMode = 0;
      const id = e.currentTarget.id;

      getEmployeeById(id).done((res) => {
        new Form("div.form-employee", id, 0, res);
      });
    });

    // event remove employee
    $(`${this.Table} table`).on("mousedown", "tbody tr", (e) => {
      if (e.which === 3) {
        $("div.pop-up div.popup-header").html("Xóa bản ghi");
        $("div.pop-up div.popup-content div.label").html(
          `Bạn có muốn chắc xóa <b>"Thông tin nhân viên củ nhân viên ${$(
            e.currentTarget
          ).attr("employeeCode")}"</b> hay không ?`
        );

        new Popup(
          "div.pop-up",
          async () => await deleteEmployee(e.currentTarget.id)
        );
      }
    });
  }

  //load data from api, render table
  loadData() {
    let tbody = $(`${this.Table} table tbody`);

    tbody.empty();

    this.GetData()
      .done((res) => {
        res.forEach((item) => {
          const tmpHtml = `<tr id=${item.EmployeeId} employeeCode=${
            item.EmployeeCode
          }
        }>
                <td class="text-align-left">${item.EmployeeCode || ""}</td>
                <td class="text-align-left">${item.FullName || ""}</td>
                <td class="text-align-center">${Common.formatDate(
                  item.DateOfBirth
                )}</td>
                <td class="text-align-left">${item.GenderName || ""}</td>
                <td class="text-align-left">${item.PhoneNumber || ""}</td>
                <td class="text-align-left">${item.Email || ""}</td>
                <td class="text-align-left">${item.PositionName || ""}</td>
                <td class="text-align-left">${item.DepartmentName || ""}</td>
                <td class="text-align-right">${
                  item.Salary
                    ? Common.formatMoney(item.Salary.toString(), " VND")
                    : ""
                }</td>
                <td class="text-align-left">${item.WorkStatus || ""}</td>
            </tr>`;
          tbody.append(tmpHtml);
        });
      })
      .fail((err) => {
        console.log(err);
      });
  }

  // cáu hình table và các trường thead
  renderTable() {
    let table = this.Table;
    $(table).append("<table><thead><tr></tr></thead><tbody></tbody></table>");
    let thead = $(`${table} table thead tr`);

    this.Props.forEach((item) => {
      thead.append(`<th class=${item.className}>${item.label}</th>`);
    });
  }
}
