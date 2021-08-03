<template>
  <div>
    <the-menu></the-menu>
    <the-header></the-header>
    <the-content></the-content>

    <!-- <combo-box
      :data="gender"
      :value="genderSearch !== null ? genderSearch : search['gender']"
      @onchangeinput="searchTextComboxbox"
      id="gender"
    >
      <template v-slot:combo-box-options>
        <combo-box-option
          v-for="item in genderCombobox"
          :key="item.id"
          :value="item.id"
          :label="item.label"
          :checked="item.id === search.gender"
          @select-item="selectItem"
        ></combo-box-option>
      </template>
    </combo-box> -->
  </div>
</template>

//
<script>
import TheHeader from "./layout/TheHeader.vue";
import TheMenu from "./layout/menu/TheMenu.vue";
import TheContent from "./layout/TheContent.vue";
import _ from "lodash";
export default {
  components: {
    "the-header": TheHeader,
    "the-menu": TheMenu,
    "the-content": TheContent,
  },
  data() {
    return {
      gender: [
        { id: "0", label: "Nữ" },
        { id: "1", label: "Nam" },
        { id: "2", label: "Không xác định" },
      ],
      genderSearch: null,
      search: {
        gender: "2",
      },
    };
  },
  computed: {
    genderCombobox() {
      const text = this.genderSearch;
      if (text === "" || text === undefined || text === null) {
        return _.cloneDeep(this.gender);
      } else {
        return _.filter(
          this.gender,
          (item) => item.label.toLowerCase().indexOf(text.toLowerCase()) > -1
        );
      }
    },
  },
  methods: {
    /*
      Xử lý chọn option trong cac combobox
    */
    selectItem(item) {
      this.search[item.key] = item.id;
      this.genderSearch = null;
    },

    /*
      Tìm kiếm theo text trong combobox
    */
    searchTextComboxbox({ value }) {
      let text = _.trim(value);
      this.genderSearch = text;
    },
  },
};
</script>

<style lang="css">
@import url("./css/common/Main.css");
</style>
