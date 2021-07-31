import BaseAPI from "@/api/base/BaseAPI.js";

class PositionAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "v1/Positions";
  }
}

export default new PositionAPI();
