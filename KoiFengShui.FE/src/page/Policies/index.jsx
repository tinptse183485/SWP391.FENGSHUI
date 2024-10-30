import React, { useState } from 'react';
import './index.css';
import HeaderTemplate from '../../components/header-page';
import FooterTemplate from '../../components/footer-page';
const Policies = () => {
    const [selectedPolicy, setSelectedPolicy] = useState('privacy');
  
    const policies = {
      privacy: {
        title: "Chính sách bảo mật",
        content: "Chúng tôi cam kết bảo vệ thông tin cá nhân của bạn. Thông tin chi tiết về cách chúng tôi thu thập, sử dụng và bảo vệ dữ liệu của bạn."
      },
      terms: {
        title: "Điều khoản sử dụng",
        content: "Bằng cách sử dụng trang web của chúng tôi, bạn đồng ý với các điều khoản và điều kiện được nêu ở đây. Khách hàng đăng bài quảng cáo phải liên quan đến cá Koi. Nội dung bài đăng không được có nội dung nhạy cảm, hay có nội dung không phù hợp với pháp Luật Việt Nam. Quý khách lưu ý phải thanh toán trước khi bài đăng được phê duyệt. Nếu quảng cáo có nội dung không hợp lệ, bài đăng sẽ bị hủy và chúng tôi sẽ hoàn tiền trong thời từ 30 - 35 ngày. Mong quý khách đọc kỹ nội dung điều khoản sử dụng trước khi đăng bài."
      },
      refund: {
        title: "Chính sách hoàn trả",
        content: "Thông tin về quy trình hoàn trả và các điều kiện áp dụng cho các sản phẩm hoặc dịch vụ của chúng tôi."
      },
      shipping: {
        title: "Chính sách vận chuyển",
        content: "Chi tiết về các phương thức vận chuyển, thời gian giao hàng và chi phí liên quan."
      }
    };
  
    return (
        <div className="policies-page">
        <HeaderTemplate />
        <div className="policies-content">
          <div className="policies-container">
            <h1 className="policies-title">Chính sách của chúng tôi</h1>
            <div className="policies-layout">
              <div className="policies-menu">
                {Object.keys(policies).map(key => (
                  <button
                    key={key}
                    className={`policy-button ${selectedPolicy === key ? 'active' : ''}`}
                    onClick={() => setSelectedPolicy(key)}
                  >
                    {policies[key].title}
                  </button>
                ))}
              </div>
              <div className="policy-display">
                <h2>{policies[selectedPolicy].title}</h2>
                <p>{policies[selectedPolicy].content}</p>
              </div>
            </div>
          </div>
        </div>
        <FooterTemplate />
        </div>
    );
  };
<<<<<<< HEAD
=======
  

>>>>>>> 8f601896338e038d395251009188d6841ebe579c
export default Policies;
