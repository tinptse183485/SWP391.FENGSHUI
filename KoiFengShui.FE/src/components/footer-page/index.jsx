import "./index.css";

function FooterPage() {
  return (
    <div>
      <footer className="main-footer">
        <div className="footer-content">
          <div className="company-info">
            <p>&copy; 2024 Company Name</p>
            <ul className="footer-links">
              <li>
                <a href="/new-products">New Products</a>
              </li>
              <li>
                <a href="/special-discounts">Special Discounts</a>
              </li>
              <li>
                <a href="/faq">FAQ</a>
              </li>
              <li>
                <a href="/blog">Blog</a>
              </li>
              <li>
                <a href="/partners">Our Partners: [Partner Logos]</a>
              </li>
            </ul>
            <div className="service-branches">
              <p>Service Branches:</p>
              <p>Showroom 1: 123 ABC, District 1, Ho Chi Minh City</p>
              <p>Showroom 2: 456 DEF, District 2, Hanoi</p>
            </div>
          </div>
          <div className="customer-services">
            <h3>Customer services</h3>
            <ul>
              <li>Warranty and return policy</li>
              <li>Privacy policy</li>
              <li>Purchase guide</li>
              <li>Terms of use</li>
              <li>Payment policy</li>
            </ul>
          </div>
        </div>
        <div className="contact-info">
          <p>Phone: 0939 158 128</p>
          <p>Email: contact@yourwebsite.com</p>
        </div>
        <div className="copyright">
          <p>Copyright &copy; 2024 All rights reserved</p>
        </div>
      </footer>
    </div>
  );
}

export default FooterPage;
