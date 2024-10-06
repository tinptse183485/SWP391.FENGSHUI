import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Home from "./page/home";
import Login from "./page/login";
import Register from "./page/register";
import Dashboard from "./page/Admin-page/dashboard";
import Calculation from "./page/calculation";
import Consulting from "./page/consulting";
import CalculateCompability from "./page/calculate-compability";
import User from "./page/Admin-page/User";
import ADS from "./page/Admin-page/Ads";
import Blog from "./page/Admin-page/blog";
import Koi from "./page/Admin-page/koi";
import Pond from "./page/Admin-page/pond";
import Ads_list from "./page/Ads_list";
function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Home />,
    },
    {
      path: "login",
      element: <Login />,
    },
    {
      path: "register",
      element: <Register />,
    },
    {
      path: "dashboard",
      element: <Dashboard />,
    },
    {

      path: "calculation",
      element: <Calculation />,
    },
    {
      path: "consulting",
      element: <Consulting />,
    },
    {
      path: "calculate-compability",
      element: <CalculateCompability />,
    },
    {
      path: "user",
      element: <User />,
    },
    {
      path: "ads",
      element: <ADS />,
    },
    {
      path: "blog",
      element: <Blog />,
    },
    {
      path: "koi",
      element: <Koi />,
    },
    {
      path: "pond",
      element: <Pond />,
    },
    {
      path: "ads-list",
      element: <Ads_list />,
    },
  ]);

  return <RouterProvider router={router} />;
}


export default App;
