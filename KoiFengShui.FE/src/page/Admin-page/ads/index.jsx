import React, { useEffect, useState } from "react";
import api from "../../../config/axios";
import moment from "moment"; // Add this import for date formatting

import {
  Table,
  Button,
  message,
  Image,
  Typography,
  Space,
  Menu,
  Modal,
  Select,
  Form,
} from "antd";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const { Text } = Typography;

const Ads = () => {
  const navigate = useNavigate();
  const [data, setData] = useState([]); // Khởi tạo state để lưu trữ dữ liệu
  const [filteredData, setFilteredData] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState("all");
  const [elements, setElements] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [currentAdId, setCurrentAdId] = useState(null);
  const [currentStatus, setCurrentStatus] = useState(null);
  const [form] = Form.useForm();
  const [isApproving, setIsApproving] = useState(false);
  const [currentElementId, setCurrentElementId] = useState(null);

  const menuItems = [
    { key: "all", label: "Tất cả" },
    { key: "Pending", label: "Đang chờ" },
    { key: "Approved", label: "Đã duyệt" },
    { key: "Canceled", label: "Đã hủy" },
    { key: "Refunded", label: "Đã hoàn tiền" },
  ];

  const fetchData = async () => {
    try {
      const response = await api.get(
        "Advertisement/GetAllAdvertisemenWithPackageSortted"
      );
      setData(response.data);
      filterAds("all");
    } catch (error) {
      console.error("Lỗi khi tải danh sách quảng cáo:", error);
      toast.error(error.response.data || "Không thể tải danh sách quảng cáo");
    }
  };

  useEffect(() => {
    fetchData();
    fetchElements();
  }, []);

  useEffect(() => {
    filterAds(selectedStatus);
  }, [selectedStatus, data]);

  const filterAds = (status) => {
    if (status === "all") {
      setFilteredData(data);
    } else {
      const filtered = data.filter((ad) => ad.status === status);
      setFilteredData(filtered);
    }
  };

  const handleMenuClick = (e) => {
    setSelectedStatus(e.key);
  };

  const rankPriority = {
    Diamond: 1,
    Gold: 2,
    Silver: 3,
  };

  const statusPriority = {
    Pending: 1,
    Approved: 2,
    Canceled: 3,
    Refunded: 4,
  };

  const columns = [
    {
      title: "Tiêu đề",
      dataIndex: "heading",
      key: "heading",
      ellipsis: true,
    },
    {
      title: "Hình ảnh",
      dataIndex: "image",
      key: "image",
      render: (image) => (
        <Image
          src={image}
          alt="Ad"
          width={80}
          height={80}
          style={{ objectFit: "cover", borderRadius: "4px" }}
        />
      ),
    },
    {
      title: "Nội dung",
      key: "content",
      render: (_, record) => (
        <Button
          type="link"
          onClick={() => navigate(`/advertisement-detail/${record.adId}`)}
        >
          Xem chi tiết
        </Button>
      ),
    },
    {
      title: "Mệnh",
      dataIndex: "elementId",
      key: "elementId",
      render: (elementId) => (
        <Text
          style={{
            color:
              elementId === "Kim"
                ? "#FFD700" // Vàng đậm
                : elementId === "Mộc"
                ? "#228B22" // Xanh lá cây đậm
                : elementId === "Thủy"
                ? "#1E90FF" // Xanh dương
                : elementId === "Hỏa"
                ? "#FF4500" // Đỏ cam
                : elementId === "Thổ"
                ? "#8B4513" // Nâu đất
                : "#000000", // Màu mặc định nếu không khớp
            fontWeight: "bold",
          }}
        >
          {elementId}
        </Text>
      ),
    },
    {
      title: "Trạng thái",
      dataIndex: "status",
      key: "status",
      render: (status) => (
        <Text
          style={{
            color:
              status === "Approved"
                ? "#52c41a"
                : status === "Refunded"
                ? "#faad14"
                : status === "Canceled"
                ? "#f5222d"
                : "#1890ff", // Màu cho trạng thái Pending
          }}
        >
          {status}
        </Text>
      ),

      sorter: (a, b) => statusPriority[a.status] - statusPriority[b.status],
      sortDirections: ["descend", "ascend"],
    },
    {
      title: "Rank",
      dataIndex: "rank",
      key: "rank",
      render: (rank) => <Text>{rank || "N/A"}</Text>,
      sorter: (a, b) =>
        (rankPriority[a.rank] || Infinity) - (rankPriority[b.rank] || Infinity),
      sortDirections: ["descend", "ascend"],
    },
    {
      title: "Ngày tạo",
      dataIndex: "createAt",
      key: "createAt",
      render: (createAt) =>
        createAt ? moment(createAt).format("DD/MM/YYYY HH:mm:ss") : "N/A",
    },
    {
      title: "Ngày bắt đầu",
      dataIndex: "startDate",
      key: "startDate",
      render: (startDate) =>
        startDate ? moment(startDate).format("DD/MM/YYYY HH:mm:ss") : "N/A",
    },
    {
      title: "Ngày hết hạn",
      dataIndex: "expiredDate",
      key: "expiredDate",
      render: (expiredDate) =>
        expiredDate ? moment(expiredDate).format("DD/MM/YYYY HH:mm:ss") : "N/A",
    },
    {
      title: "Duyệt bài",
      key: "action",
      width: 200,

      render: (_, record) => {
        const isApproved = record.status === "Approved";
        const isRefunded = record.status === "Refunded";
        const isCanceled = record.status === "Canceled";
        const isDisabled = isApproved || isRefunded || isCanceled;
        return (
          <Space size="small" direction="vertical">
            <Button
              type="primary"
              onClick={() => handleUpdateStatus(record.adId, "Approved")}
              disabled={isDisabled}
            >
              Phê duyệt
            </Button>
            {isCanceled ? (
              <Button
                type="primary"
                danger
                onClick={() => handleUpdateStatus(record.adId, "Refunded")}
                disabled={isApproved || isRefunded}
              >
                Hoàn trả
              </Button>
            ) : (
              <Button
                type="primary"
                danger
                onClick={() => handleUpdateStatus(record.adId, "Canceled")}
                disabled={isApproved || isRefunded}
              >
                Hủy bỏ
              </Button>
            )}
          </Space>
        );
      },
    },
  ];

  const fetchElements = async () => {
    try {
      const response = await api.get("Element/GetAllElement");
      setElements(response.data);
    } catch (error) {
      console.error("Error fetching elements:", error);
      toast.error(error.response.data ||"Không thể tải danh sách element");
    }
  };

  const handleUpdateStatus = (adId, status) => {
    const ad = data.find((item) => item.adId === adId);
    setCurrentAdId(adId);
    setCurrentStatus(status);
    setIsApproving(status === "Approved");
    setCurrentElementId(ad.elementId); // Set the current element ID
    setIsModalVisible(true);

    if (status === "Approved") {
      form.setFieldsValue({ elementId: ad.elementId }); // Pre-set the form field
    }
  };

  const handleModalOk = async () => {
    try {
      if (isApproving) {
        const values = await form.validateFields();
        await api.put(
          `Advertisement/ApproveAdvertisement?adId=${currentAdId}&elementID=${values.elementId}&status=${currentStatus}`
        );
      } else {
        // Sử dụng API mới cho hủy bỏ/hoàn tiền
        await api.put(
          `Advertisement/UpdateAdvertisementStatus?adId=${currentAdId}&status=${currentStatus}`
        );
      }

      toast.success(
        `Cập nhật trạng thái thành công của bài quảng cáo ${currentAdId} thành ${currentStatus}`
      );
      fetchData();
      resetModalData();
    } catch (error) {
      console.error("Error updating status:", error);
      toast.error(
        error.response.data || `Cập nhật trạng thái thất bại của bài quảng cáo ${currentAdId}`
      );
    }
  };

  const handleModalCancel = () => {
    resetModalData();
  };

  const resetModalData = () => {
    setIsModalVisible(false);
    setCurrentAdId(null);
    setCurrentStatus(null);
    setIsApproving(false);
    form.resetFields();
  };

  return (
    <div>
      <h1>Quản lý quảng cáo</h1>
      <Menu
        mode="horizontal"
        selectedKeys={[selectedStatus]}
        onClick={handleMenuClick}
        style={{ marginBottom: 16 }}
      >
        {menuItems.map((item) => (
          <Menu.Item key={item.key}>{item.label}</Menu.Item>
        ))}
      </Menu>
      <Table dataSource={filteredData} columns={columns} />
      <Modal
        title={
          isApproving
            ? "Phê duyệt quảng cáo"
            : `${
                currentStatus === "Canceled" ? "Hủy bỏ" : "Hoàn tiền"
              } quảng cáo`
        }
        visible={isModalVisible}
        onOk={handleModalOk}
        onCancel={handleModalCancel}
      >
        {isApproving ? (
          <Form form={form} initialValues={{ elementId: currentElementId }}>
            <Form.Item
              name="elementId"
              rules={[{ required: true, message: "Vui lòng chọn một element" }]}
            >
              <Select
                style={{ width: "100%" }}
                onChange={(value) => setCurrentElementId(value)}
              >
                {elements.map((element) => (
                  <Select.Option
                    key={element.elementId}
                    value={element.elementId}
                  >
                    {element.elementId}
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
          </Form>
        ) : (
          <p>
            Bạn có chắc chắn muốn{" "}
            {currentStatus === "Canceled" ? "hủy bỏ" : "hoàn tiền"} quảng cáo
            này?
          </p>
        )}
      </Modal>
    </div>
  );
};

export default Ads;
