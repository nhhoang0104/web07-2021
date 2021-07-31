<template>
  <div class="content">
    <div class="content__header">
      <div class="content__header__left">
        <div class="text text--heading">Danh sách nhân viên</div>
      </div>
      <div class="content__header__right">
        <base-button
          :className="
            `btn--danger padding-left--16 padding-right--16 margin-right--10 ${
              dataRemove.length === 0 ? 'btn--hidden' : ''
            }`
          "
        >
          <div class="text text--color-white text--center">
            Xóa nhân viên
          </div>
        </base-button>
        <base-button className="padding-left--16 padding-right--16">
          <div class="icon icon--13 icon--add" style="margin-right:8px"></div>
          <div class="text text--color-white text--center">
            Thêm nhân viên
          </div>
        </base-button>
      </div>
    </div>
    <div class="content__toolbar">
      <div class="content__toolbar__left">
        <base-input></base-input>
        <combo-box
          :label="departmentSelected.label"
          @onchangeinput="search"
          id="department"
        >
          <template v-slot:combo-box-options>
            <combo-box-option
              v-for="item in department"
              :key="item.id"
              :value="item.id"
              :label="item.label"
              :checked="item.checked"
              @select-item="selectItem"
            ></combo-box-option>
          </template>
        </combo-box>
        <combo-box
          :label="positionSelected.label"
          @onchangeinput="search"
          id="position"
        >
          <template v-slot:combo-box-options>
            <combo-box-option
              v-for="item in position"
              :key="item.id"
              :value="item.id"
              :label="item.label"
              :checked="item.checked"
              @select-item="selectItem"
            ></combo-box-option>
          </template>
        </combo-box>
      </div>
      <div class="content__toolbar__right"></div>
    </div>
    <div class="content__body">
      <div class="content__body__grid">
        <base-table>
          <template v-slot:thead>
            <base-table-head :columns="columns"></base-table-head>
          </template>
          <template v-slot:tbody>
            <base-table-body
              :columns="columns"
              :data="data"
              @check-box="checkbox"
            ></base-table-body>
          </template>
        </base-table>
      </div>
    </div>
    <div class="content__footer">
      <base-pagination></base-pagination>
    </div>
  </div>
</template>

<script>
import EmployeesAPI from "@/api/components/EmployeesAPI.js";
import PositionAPI from "@/api/components/PositionAPI.js";
import DepartmentAPI from "@/api/components/DepartmentAPI.js";
import { columns } from "@/pages/employee/Column.js";

export default {
  data() {
    return {
      columns: columns,
      data: [],
      dataRemove: [],
      gender: [
        { id: "0", label: "Nữ", checked: false },
        { id: "1", label: "Nam", checked: false },
        { id: "2", label: "Không xác định", checked: false },
      ],
      department: [],
      position: [],
      departmentSelected: {
        value: "1",
        label: "Tất cả phòng ban",
      },
      positionSelected: {
        value: "1",
        label: "Tất cả vị trí",
      },
    };
  },
  created() {
    PositionAPI.getAll().then((res) => {
      let tmp = res.data.map(({ PositionId, PositionName }) => ({
        id: PositionId,
        label: PositionName,
        checked: false,
      }));

      tmp.push({ id: "1", label: "Tất cả vị trí", checked: true });
      this.position = tmp;
    });

    DepartmentAPI.getAll().then((res) => {
      let tmp = res.data.map(({ DepartmentId, DepartmentName }) => ({
        id: DepartmentId,
        label: DepartmentName,
        checked: false,
      }));

      tmp.push({ id: "1", label: "Tất cả phòng ban", checked: true });
      this.department = tmp;
    });
  },
  methods: {
    /*
        Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      let tmp = [];
      let itemSelected = {};
      let id = item.id;

      switch (item.key) {
        case "department":
          tmp = this.department;
          itemSelected = this.departmentSelected;
          break;
        case "position":
          tmp = this.position;
          itemSelected = this.positionSelected;
          break;
      }

      tmp.forEach((e) => {
        if (e.id === id) {
          e.checked = true;
          itemSelected.value = id;
          itemSelected.label = e.label;
        } else {
          e.checked = false;
        }
      });
    },

    search(value) {
      console.log(value);
    },

    checkbox(id) {
      let tmp = this.data.find((item) => item.EmployeeId == id);
      tmp.checked = !tmp.checked;
      if (tmp.checked) this.dataRemove.push(id);
      else {
        const index = this.dataRemove.indexOf(id);
        if (index > -1) {
          this.dataRemove.splice(index, 1);
        }
      }
    },
  },

  mounted() {
    /*
        Lấy dữ liệu nhân viên từ api
    */
    EmployeesAPI.getAll()
      .then((res) => {
        this.data = res.data;
      })
      .catch((err) => console.log(err));
  },
};
</script>

<style lang="css" scoped>
@import url("../../css/page/Employee.css");
</style>
