import "./index.css";

function FooterPage() {
  return (
    <div>
      <footer className="main-footer">
        <div className="footer-content">
          <div className="company-info">
            <p>&copy; 2024 KOI PHONG THỦY</p>
            
            <div className="contact-info">
              <p>Điện thoại: 086 8686 868</p>
              <p>Email: koifengshui@gmail.com</p>
            </div>
            <div className="copyright">
              <p>Bản quyền &copy; 2024 Đã đăng ký bản quyền</p>
            </div>
          </div>

          <div className="customer-services">
            <h3>Dịch vụ khách hàng</h3>
            <ul>
              <li>Chính sách bảo hành và đổi trả</li>
              <li>Chính sách bảo mật</li>
              <li>Hướng dẫn mua hàng</li>
              <li>Điều khoản sử dụng</li>
              <li>Chính sách thanh toán</li>
            </ul>
          </div>
        </div>
      </footer>
    </div>
  );
}

export default FooterPage;
