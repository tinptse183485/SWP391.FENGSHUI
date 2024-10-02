import "./index.css";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
function Consulting() {
  return (
    <div>
      <header>
        <HeaderTemplate></HeaderTemplate>
      </header>
      <body>
        <div className="Header-fish">
          <h2>Các loại cá phù hợp</h2>
        </div>
        <div className="Header-pond">
          <h2> Đặc điểm hồ phù hợp</h2>

          <h3>Hình dạng:</h3>

          <h3>Hướng:</h3>
        </div>
      </body>
      <footer>
        <FooterPage></FooterPage>
      </footer>
    </div>
  );
}
export default Consulting;
