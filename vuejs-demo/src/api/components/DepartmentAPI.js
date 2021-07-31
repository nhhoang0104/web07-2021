import BaseAPI from "@/api/base/BaseAPI.js";

class DepartmentAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "api/Department";
  }
}

export default new DepartmentAPI();
