<<<<<<< HEAD
import {
  RouterProvider,
  createBrowserRouter,
  Navigate,
  useLocation,
} from "react-router-dom";
import { toast } from "react-toastify";
=======
import { RouterProvider, createBrowserRouter, Navigate, useLocation } from "react-router-dom";
import { toast } from 'react-toastify';
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
import Home from "./page/home";
import Login from "./page/login";
import Register from "./page/register";

<<<<<<< HEAD
=======

>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
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
=======
import Dashboard from "./components/dashboard";


import AdvertisementDetail from "./page/AdvertismentDetail";
import PaymentSuccess from './page/PaymentSuccess';
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
import Dashboard from "./components/dashboard";

import AdvertisementDetail from "./page/AdvertismentDetail";
import PaymentSuccess from "./page/PaymentSuccess";
import VNPayPayment from "./page/Payment";
import ChoosePackage from "./page/ChoosePackage";
import CreateBlog from "./page/Admin-page/CreateBlog";
import BlogDetail from "./page/Admin-page/BlogDetail";
import BlogList from "./page/Blogs-list";
import AdminDashboard from "./page/Admin-page/dashboard";
import UserProfile from "./page/User-Profile";
import ForgotPassword from "./page/Forgot-Password";
import ResetPassword from "./page/Reset-Password";
<<<<<<< HEAD
=======

>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6

function App() {
  const ProtectedRoute = ({ children }) => {
    const location = useLocation();
<<<<<<< HEAD
    const userId = localStorage.getItem("userId");

    if (!userId) {
      toast.error("Vui lòng đăng nhập để truy cập trang này.");
=======
    const userId = localStorage.getItem('userId');

    if (!userId) {
      toast.error('Vui lòng đăng nhập để truy cập trang này.');
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
      return <Navigate to="/login" state={{ from: location }} replace />;
    }

    return children;
  };

  const ProtectedRouteAdmin = ({ children }) => {
<<<<<<< HEAD
    const userId = localStorage.getItem("userId");
    const userRole = localStorage.getItem("role");
=======
    const userId = localStorage.getItem('userId');
    const userRole = localStorage.getItem('role');
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6

    if (userId && userRole === "Admin") {
      return children;
    }
<<<<<<< HEAD
    toast.error("Vui lòng đăng nhập để truy cập trang này.");
    return <Navigate to="/login" replace />;
  };
=======
    toast.error('Vui lòng đăng nhập để truy cập trang này.');
      return <Navigate to="/login"  replace />;
  }
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
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
      path: "forgot-password",
      element: <ForgotPassword />,
    },
    {
      path: "reset-password/:email",
      element: <ResetPassword />,
    },
    {
      path: "dashboard",
<<<<<<< HEAD
      element: (
        <ProtectedRouteAdmin>
          <Dashboard />
        </ProtectedRouteAdmin>
      ),
=======
      element:<ProtectedRouteAdmin><Dashboard/></ProtectedRouteAdmin> ,
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
      children: [
        {
          path: "AdminDashboard",
          element: <AdminDashboard/>,
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
          path: "create-blog",
          element: <CreateBlog />,
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
      path: "blogs-list",
      element: <BlogList />,
    },
    {
      path: "ads-list",
      element: <Ads_list />,
    },
    {
      path: "user-profile",
<<<<<<< HEAD
      element: (
        <ProtectedRoute>
          <UserProfile />
        </ProtectedRoute>
      ),
    },
    {
      path: "create-ads",
      element: (
        <ProtectedRoute>
          <CreateAds />
        </ProtectedRoute>
      ),
    },
    {
      path: "user-ads",
      element: (
        <ProtectedRoute>
          <User_Ads />
        </ProtectedRoute>
      ),
=======
      element: <ProtectedRoute><UserProfile /></ProtectedRoute>,
    },
    {
      path: "create-ads",
      element: <ProtectedRoute><CreateAds /></ProtectedRoute>,
    },
    {
      path: "user-ads",
      element: <ProtectedRoute><User_Ads /></ProtectedRoute>,
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
    },

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
      path: "blog-detail/:id",
      element: <BlogDetail />,
    },
    {
      path: "/choose-package",
<<<<<<< HEAD
      element: <ChoosePackage />,
    },
=======
      element: <ChoosePackage />
    },
  
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
  ]);

  return <RouterProvider router={router} />;
}

<<<<<<< HEAD
export default App;
=======
export default App;
>>>>>>> fcc6ed334b5314b956076ceb0b29dd06c4373ed6
