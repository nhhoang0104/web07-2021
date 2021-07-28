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
        <dropdown
          :label="itemSelected.label || 'Chọn giới tính'"
          :valueSelected="itemSelected.value"
        >
          <template v-slot:dropdown-options>
            <dropdown-option
              v-for="item in gender"
              :key="item.id"
              :value="item.id"
              :label="item.label"
              :checked="item.checked"
              @select-item="selectItem"
            ></dropdown-option>
          </template>
        </dropdown>
      </div>
      <div class="conntent__toolbar__right"></div>
    </div>
    <div class="content__body">
      <base-table>
        <template v-slot:thead>
          <base-table-head :column="column"></base-table-head>
        </template>
        <template v-slot:tbody>
          <base-table-body :column="column" :data="data"></base-table-body>
        </template>
      </base-table>
    </div>
    <div class="conntent__footer"></div>
  </div>
</template>

<script>
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
      column: [
        { id: "name", label: "Ten", className: "text text--left" },
        { id: "date", label: "Ngay sinh", className: "text text--center" },
        { id: "money", label: "Tien", className: "text text--left" },
        { id: "position", label: "Vi tri", className: "text text--left" },
      ],
      data: [
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
        {
          name: "hoang",
          date: "12/12/1212",
          money: "1312312",
          position: "testasasa",
        },
      ],
    };
  },
  methods: {
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
  },
};
</script>

<style lang="css">
@import url("../../css/layout/Content.css");
</style>
