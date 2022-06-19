import { Axios } from "axios";


const http = new Axios({
  baseURL: "http://localhost:5010/api/"
});

export default http;