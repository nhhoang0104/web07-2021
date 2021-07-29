<template>
  <div class="content">
    <div class="content__header">
      <div class="content__header__left">
        <div class="text text--heading">Danh sách nhân viên</div>
      </div>
      <div class="conetent__header__right"></div>
    </div>
    <div class="content__toolbar">
      <div class="conntent__toolbar__left">
        <combo-box
          :label="itemSelected.label || 'Chọn giới tính'"
          :valueSelected="itemSelected.value"
          @onchangeinput="search"
        >
          <template v-slot:combo-box-options>
            <combo-box-option
              v-for="item in gender"
              :key="item.id"
              :value="item.id"
              :label="item.label"
              :checked="item.checked"
              @select-item="selectItem"
            ></combo-box-option>
          </template>
        </combo-box>
      </div>
      <div class="conntent__toolbar__right"></div>
    </div>
    <div class="content__body">
      <div class="content__body__grid">
        <base-table>
          <template v-slot:thead>
            <base-table-head :columns="columns"></base-table-head>
          </template>
          <template v-slot:tbody>
            <base-table-body :columns="columns" :data="data"></base-table-body>
          </template>
        </base-table>
      </div>
    </div>
    <div class="conntent__footer"></div>
  </div>
</template>

<script>
import EmployeesAPI from "@/api/components/EmployeesAPI.js";
import { columns } from "@/page/employee/Column.js";

export default {
  name: "the-content",

  data() {
    return {
      gender: [
        { id: "0", label: "Nữ", checked: false },
        { id: "1", label: "Nam", checked: false },
        { id: "2", label: "Không xác định", checked: true },
      ],
      itemSelected: {
        label: null,
        value: null,
      },
      columns: columns,
      data: [],
    };
  },
  methods: {
    /*
        Xử lý chọn option trong dropdown
     */

    selectItem(id) {
      this.gender.forEach((item) => {
        if (item.id === id) {
          item.checked = true;
          this.itemSelected.value = id;
          this.itemSelected.label = item.label;
        } else {
          item.checked = false;
        }
      });
    },

    search(value) {
      console.log(value);
    },
  },
  created() {
    /* 
        Lấy dữ liệu nhân viên từ dropdown
    */
    EmployeesAPI.getAll()
      .then((res) => {
        this.data = res.data;
      })
      .catch((err) => console.log(err));
  },
};
</script>

<style lang="css">
@import url("../../css/layout/Content.css");
</style>
