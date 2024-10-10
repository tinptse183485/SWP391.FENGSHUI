import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Home from "./page/home";
import Login from "./page/login";
import Register from "./page/register";

// import Dashboard from "./page/Admin-page/dashboard";

import Calculation from "./page/calculation";
import Consulting from "./page/consulting";
import CalculateCompability from "./page/calculate-compability";
import User from "./page/Admin-page/User";
import ADS from "./page/Admin-page/Ads";
import Blog from "./page/Admin-page/blog";
import Koi from "./page/Admin-page/koi";
import Pond from "./page/Admin-page/pond";
import Ads_list from "./page/Ads_list";
import CreateAds from "./page/CreateAds";
import User_Ads from "./page/User_Ads";
<<<<<<< HEAD
import Dashboard from "./components/dashboard";
import AdvertisementDetail from "./page/AdvertismentDetail";
import VNPayPayment from './page/Payment';
import PaymentSuccess from './page/PaymentSuccess';
import ChoosePackage from './page/ChoosePackage';
import AdminDashboard from "./page/Admin-page/dashboard";

=======
>>>>>>> a21a9d7812688128b7b244473b2e0e44a23f3f0e

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
      element: <Dashboard/>,
      children: [
        {
          path: "AdminDashboard",
          element: <AdminDashboard />,
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
      ],
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
      path: "ads-list",
      element: <Ads_list />,
    },
    {
      path: "create-ads",
      element: <CreateAds />,
    },
    {
      path: "user-ads",
      element: <User_Ads />,
    },

<<<<<<< HEAD
    {
      path: "advertisement-detail/:id",
      element: <AdvertisementDetail />,
    },
    {
      path: "payment",
      element: <VNPayPayment />,
    },
    {
      path: "payment-success",
      element: <PaymentSuccess />,
    },
    {
      path: "/choose-package",
      element: <ChoosePackage />
    },


=======
>>>>>>> a21a9d7812688128b7b244473b2e0e44a23f3f0e
  ]);

  return <RouterProvider router={router} />;
}


export default App;
