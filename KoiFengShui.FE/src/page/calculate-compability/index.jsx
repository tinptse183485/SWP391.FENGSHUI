import { useEffect, useState } from "react";

import {
  Form,
  DatePicker,
  Select,
  Button,
  Radio,
  Input,
  Modal,
  Slider,
  InputNumber,
} from "antd";


import api from "../../config/axios";
import { toast } from "react-toastify";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
import "./index.css";

const { Option } = Select;

function ComputeCompability() {
  const [form] = Form.useForm();

  

  const [filteredFishList, setFilteredFishList] = useState([]);
  const [fishList, setFishList] = useState([]);
  const [selectedFishes, setSelectedFishes] = useState([]);
  const [fishPoints, setFishPoints] = useState({});
  const [pondShapes, setPondShapes] = useState([]);
  const [selectedPondShape, setSelectedPondShape] = useState(null);
  const [pondDirections, setPondDirections] = useState([]);
  const [compatibilityResult, setCompatibilityResult] = useState(null);
  const [elements, setElements] = useState([]);
  const [elementFilter, setElementFilter] = useState("all");
  const [userElement, setUserElement] = useState(null);
  const [colors, setColors] = useState([]);
  const [colorFilter, setColorFilter] = useState("all");
  // Tạo state cho modal và thông tin về các màu của cá
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedFishDetail, setSelectedFishDetail] = useState(null);
  const [colorWeights, setColorWeights] = useState([]);
  const [userLifePalife, setUserLifePalife] = useState(null);
  const [allFishColorWeights, setAllFishColorWeights] = useState({});
  const [allFishColors, setAllFishColors] = useState({});

  const [shapePoint, setShapePoint] = useState(null);
  const [directionPoint, setDirectionPoint] = useState(null);


  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    const fetchFilteredFishList = async () => {
      let tempFilteredFishList = [];

      if (colorFilter === "all") {
        // Nếu không chọn màu, chỉ lọc theo element
        tempFilteredFishList = fishList.filter(
          (fish) => elementFilter === "all" || fish.element === elementFilter
        );
      } else {
        // Gọi API để lấy cá theo màu
        try {
          const response = await api.get(
            `KoiVariety/GetListKoiByColor?color=${colorFilter}`
          );
          const fishByColor = response.data;

          // Lọc theo bảng mệnh sau khi có danh sách cá theo màu
          tempFilteredFishList = fishByColor.filter(
            (fish) => elementFilter === "all" || fish.element === elementFilter
          );
        } catch (error) {
          toast.error("Error fetching fish by color");
          return; // Trả về để không cập nhật danh sách cá nếu có lỗi
        }
      }

      setFilteredFishList(tempFilteredFishList);
    };

    fetchFilteredFishList();
  }, [colorFilter, elementFilter, fishList]);


  useEffect(() => {
    if (selectedFishDetail && colorWeights) {
      form.setFieldsValue({
        selectedFish: {
          koiType: selectedFishDetail.koiType,
          colors: colorWeights
        }
      });
    }
  }, [selectedFishDetail, colorWeights]);


  const fetchData = async () => {
    try {
      const [
        fishResponse,
        shapeResponse,
        directionResponse,
        elementResponse,
        colorResponse,

        allColorsResponse

      ] = await Promise.all([
        api.get("KoiVariety/GetAllKoi"),
        api.get("Shape/GetAllShape"),
        api.get("Direction/GetAllDirection"),
        api.get("Element/GetAllElement"),
        api.get("Color/GetAllColor"),

        api.get("TypeColor/GetAllTypeColor")

      ]);
      setFishList(fishResponse.data);
      setFilteredFishList(fishResponse.data); // Đặt danh sách cá ban đầu
      setPondShapes(shapeResponse.data);
      setPondDirections(directionResponse.data);
      setElements(elementResponse.data);
      setColors(colorResponse.data);

      
      
      const allColors = allColorsResponse.data;
      const fishColorsMap = {};
      allColors.forEach(color => {
        if (!fishColorsMap[color.koiType]) {
          fishColorsMap[color.koiType] = [];
        }
        fishColorsMap[color.koiType].push({
          colorId: color.colorId,
          percentage: color.percentage || 0,
          originalPercentage: color.percentage || 0  // Add this line
        });
      });
      setAllFishColors(fishColorsMap);
      

    } catch (error) {
      toast.error("Error fetching data");
    }
  };

  const onFinish = async (values) => {
    try {
      const {
        birthdate,
        Gender,
        selectedPondShape,
        pondDirection,
      } = values;

      // Chuẩn bị payload

      const payload = selectedFishes.map(fish => ({
        koiType: fish.koiType,
        colors: fish.colors.map(color => ({
          colorId: color.colorId,
          percentage: color.percentage,
        })).filter(color => color.percentage > 0)
      }));


      console.log("Payload gửi đi:", payload);

      // Gọi API sử dụng query params thay vì body
      const response = await api.post(

        `Compatibility/GetTheCompatibilityOfUserByListFish?ShapeID=${selectedPondShape}&Direction=${pondDirection}&DOB=${birthdate.format("YYYY-MM-DD")}&Gender=${Gender}`,
        payload
      );

      console.log("API Response:", response.data);
      
      const response1 = await api.get(`Fate/element?dob=${birthdate.format("YYYY-MM-DD")}`);
      const response2 = await api.get(`Fate/CalculateLife_Palace?YOB=${birthdate.format("YYYY-MM-DD")}&gender=${Gender}`);

      setUserElement(response1.data);
      setUserLifePalife(response2.data);

      if (response.data) {
        toast.success("Calculation successful");
        setCompatibilityResult(response.data.compatibility);
      } else {
        toast.error("No data received from the server");
      }
    } catch (error) {
      console.error("API error:", error);
      toast.error(error.response?.data || "Error calculating compatibility");
      setCompatibilityResult(null);
    }
  };
  function getCompatibilityComment(compatibilityResult) {

    let comment = "";
    if (compatibilityResult < 25) {
      comment+= "Độ tương thích quá thấp không phù hợp với gia chủ.";
    } else if (compatibilityResult >= 25 && compatibilityResult <= 50) {
      comment+= "Độ tương thích không quá tốt, gia chủ nên cân nhắc.";
    } else if (compatibilityResult > 50 && compatibilityResult <= 75) {
      comment+= "Độ tương thích tương đối thích hợp với gia chủ.";
    } else if (compatibilityResult > 75 && compatibilityResult <= 100) {
      comment+= "Độ tương thích rất tốt với gia chủ, sẽ mang lại may mắn và tài lộc cho gia chủ.";
    } 
    
      console.log(shapePoint);
      console.log(userElement);
      console.log(fishPoints);
    
    if (userElement === "Kim" && shapePoint <= 75) {
      comment += "\n\nVới gia chủ mang mệnh Kim, hình vuông là sự lựa chọn lý tưởng, vì nó tương sinh với Thổ, tạo ra sự ổn định và bền vững. Hình tròn cũng là hình dáng tương hợp với mệnh Kim, mang lại sự hài hòa và sự phát triển. Tuy nhiên, gia chủ nên tránh hình tam giác và hình chữ nhật, vì chúng tương khắc với mệnh Kim, có thể gây ra sự bất ổn trong vận khí.";
      } else if (userElement === "Mộc" && shapePoint <= 75) {
      comment += "\n\nVới gia chủ mang mệnh Mộc, hình chữ nhật là sự lựa chọn phù hợp nhất, vì nó tương hợp với Mộc và thúc đẩy sự phát triển. Hình lượn sóng tượng trưng cho yếu tố Thủy, giúp tăng cường sinh khí cho Mộc nhưng gia chủ nên tránh hình tròn và hình vuông, vì chúng tương khắc với mệnh Mộc, có thể làm giảm năng lượng và sự phát triển.";
      } else if (userElement === "Thủy" && shapePoint <= 75) {
      comment += "\n\nVới gia chủ mang mệnh Thủy, hình tròn là lựa chọn tốt nhất, vì nó tương sinh với Kim, mang lại sự giàu có và thịnh vượng. Hình lượn sóng cũng tương hợp với Thủy, giúp duy trì sự tươi mới và dòng chảy năng lượng. Tuy nhiên, nên tránh hình vuông và hình tam giác, vì chúng tương khắc với mệnh Thủy, có thể gây rối loạn trong vận khí.";
      } else if (userElement === "Hỏa" && shapePoint <= 75) {
      comment += "\n\nVới gia chủ mang mệnh Hỏa, hình chữ nhật là hình dáng phù hợp nhất, vì nó tương sinh với Mộc, thúc đẩy sự phát triển và năng lượng. Hình tam giác là tương hợp, mang lại sức mạnh và sự quyết tâm nhưng gia chủ cần tránh hình lượn sóng và hình tròn, vì chúng tương khắc với mệnh Hỏa, có thể gây cản trở trong sự phát triển.";
      } else if (userElement === "Thổ" && shapePoint <= 75) {
      comment += "\n\nVới gia chủ mang mệnh Thổ, hình vuông là hình dáng lý tưởng, vì nó tương hợp với Thổ, giúp mang lại sự ổn định và bền vững. Hình tam giác tương sinh với Hỏa, mang lại năng lượng tích cực. Tuy nhiên, cần tránh hình lượn sóng và hình chữ nhật, vì chúng tương khắc với mệnh Thổ, có thể gây ra sự bất ổn trong vận khí.";
      } 

      if (userLifePalife === "Càn" && directionPoint <= 75) {
        comment += "\n\nVề hướng, gia chủ có thể lựa chọn Hướng Tây hoặc Đông Bắc. Hướng Tây sẽ giúp thu hút tài lộc và sự thịnh vượng, trong khi Hướng Đông Bắc lại tạo điều kiện cho sự phát triển bền vững và ổn định trong cuộc sống. Nếu có thể, hãy bố trí không gian sống sao cho luôn nhận được ánh sáng tự nhiên từ những hướng này.";
    } else if (userLifePalife === "Đoài" && directionPoint <= 75) {
        comment += "\n\nHướng Tây Nam và Tây Bắc là những hướng lý tưởng cho gia chủ. Hướng Tây Nam giúp mang lại sự hòa hợp và tình cảm trong gia đình, trong khi Hướng Tây Bắc lại tạo điều kiện thuận lợi cho sự nghiệp và thành công cá nhân. Đừng quên bố trí các vật dụng phong thủy ở những hướng này để tối ưu hóa nguồn năng lượng tích cực.";
    } else if (userLifePalife === "Cấn" && directionPoint <= 75) {
        comment += "\n\nGia chủ có thể lựa chọn hướng Tây Nam và Tây Bắc là những hướng lý tưởng cho gia chủ. Hướng Tây Nam giúp mang lại sự hòa hợp và tình cảm trong gia đình, trong khi Hướng Tây Bắc lại tạo điều kiện thuận lợi cho sự nghiệp và thành công cá nhân. Hãy thử đặt bàn làm việc của bạn ở những vị trí này để cảm nhận sự khác biệt.";
    } else if (userLifePalife === "Khôn" && directionPoint <= 75) {
        comment += "\n\nHướng Tây và Đông Bắc sẽ là những lựa chọn tốt nhất cho gia chủ. Hướng Tây sẽ mang lại sự thuận lợi trong công việc và tài chính, trong khi Hướng Đông Bắc giúp tạo dựng mối quan hệ tốt đẹp và tăng cường sức khỏe. Đừng quên tạo một không gian thoải mái và yên tĩnh để thư giãn.";
    } else if (userLifePalife === "Ly" && directionPoint <= 75) {
        comment += "\n\nGia chủ nên chọn Hướng Đông hoặc Đông Nam. Hướng Đông không chỉ giúp gia tăng năng lượng mà còn mang lại sự khởi đầu mới trong công việc, trong khi Hướng Đông Nam giúp cải thiện tài chính và tình cảm. Cố gắng tạo không gian xanh và thoáng đãng để cảm nhận sự thư giãn.";
    } else if (userLifePalife === "Khảm" && directionPoint <= 75) {
        comment += "\n\nHướng Đông Nam và Đông sẽ mang lại nhiều điều tốt đẹp cho gia chủ. Hướng Đông Nam không chỉ giúp tăng cường sức khỏe mà còn thúc đẩy sự nghiệp phát triển, trong khi Hướng Đông giúp tạo ra không gian sáng tạo và đổi mới. Hãy chú ý đến ánh sáng và màu sắc trong không gian sống.";
    } else if (userLifePalife === "Tốn" && directionPoint <= 75) {
        comment += "\n\nGia chủ có thể chọn Hướng Bắc hoặc Nam. Hướng Bắc giúp gia tăng sự nghiệp và tài chính, trong khi Hướng Nam mang lại sự phát triển cá nhân và gia đình. Hãy thử bố trí các vật phẩm phong thủy để tối ưu hóa năng lượng ở những hướng này.";
    } else if (userLifePalife === "Chấn" && directionPoint <= 75) {
        comment += "\n\nHướng Nam và Bắc sẽ mang lại năng lượng tích cực cho gia chủ. Hướng Nam không chỉ thu hút sự giàu có mà còn mang lại hạnh phúc trong gia đình, trong khi Hướng Bắc giúp thúc đẩy sự nghiệp. Hãy chú ý đến việc bày trí và sắp xếp không gian sống để tối ưu hóa năng lượng từ các hướng này.";
    }
    const fishPoint = Object.values(fishPoints).length > 0
    ? Object.values(fishPoints).reduce((sum, point) => sum + point, 0) / Object.values(fishPoints).length
    : 0;
    console.log(fishPoint);
    if (userElement === "Mộc" && fishPoint <= 0.60) {
      comment += "%0D%0AVới gia chủ mang mệnh Mộc, việc chọn những chú cá Koi phù hợp về màu sắc không chỉ giúp gia tăng vận khí mà còn góp phần quan trọng vào sự hài hòa, phát triển của người nuôi. Những màu sắc như xanh dương, đen, xanh đen và xanh lá cây đặc biệt phù hợp với mệnh Mộc, vì màu xanh dương, đen, xanh đen trong phong thủy đại diện cho yếu tố Thủy, mà theo quy luật ngũ hành, Thủy sinh Mộc, giúp thúc đẩy sự sinh sôi nảy nở và mang lại sự thịnh vượng. Màu xanh lá cây lại chính là màu bản mệnh của Mộc, mang đến sự tươi mới, cân bằng và phát triển bền vững, giúp gia chủ cảm nhận được sự an lành và tinh thần thoải mái.\n\n";
    } else if (userElement === "Thủy" && fishPoint <= 0.60) {
      comment += "%0D%0AVới gia chủ mang mệnh Thủy, việc lựa chọn những chú cá Koi có màu sắc tương hợp không chỉ giúp tăng cường vận may mà còn mang lại sự bình an, tài lộc. Các màu như xanh dương, trắng bạc, đen và xanh đen đặc biệt phù hợp với mệnh Thủy, bởi màu xanh dương đại diện cho chính bản mệnh Thủy, giúp gia tăng sự thịnh vượng. Màu trắng bạc tượng trưng cho yếu tố Kim, mà trong ngũ hành Kim sinh Thủy, giúp gia chủ thu hút được nhiều vận may và tài lộc.\n\n";
    } else if (userElement === "Hỏa" && fishPoint <= 0.60) { 
      comment += "\n Với gia chủ mang mệnh Hỏa, việc chọn cá Koi có màu sắc phù hợp sẽ giúp kích thích năng lượng tích cực, thúc đẩy sự may mắn và thành công. Các màu như đỏ, cam, hồng và tím là lựa chọn tuyệt vời cho mệnh Hỏa, bởi chúng tượng trưng cho ngọn lửa, sức mạnh và sự quyết tâm. Đặc biệt, việc kết hợp với những chú cá Koi có màu xanh lá cây hoặc xanh lục nhạt cũng rất có lợi cho người mệnh Hỏa. Màu xanh lá cây đại diện cho yếu tố Mộc, giúp gia tăng sức mạnh và hỗ trợ sự phát triển. Sự kết hợp này không chỉ tạo nên sự hài hòa mà còn kích thích năng lượng tích cực, giúp gia chủ đạt được thành công trong mọi lĩnh vực.\n\n"; 
    } else if (userElement === "Thổ" && fishPoint <= 0.60) {
      comment += "%0D%0A Với gia chủ mang mệnh Thổ, việc chọn cá Koi phù hợp về màu sắc sẽ giúp ổn định vận khí và mang lại thịnh vượng lâu dài. Các màu như vàng, nâu, cam nhạt là những lựa chọn tốt cho mệnh Thổ, vì chúng đại diện cho đất, sự vững chắc và bền bỉ. Những màu sắc này giúp tăng cường yếu tố Thổ, mang lại sự ổn định, bền vững và giúp gia chủ cảm thấy an toàn trong cuộc sống. Đặc biệt, việc kết hợp với các màu sắc như đỏ hoặc cam từ mệnh Hỏa cũng rất có lợi, vì Hỏa sinh Thổ, thúc đẩy sự phát triển và mang lại nguồn năng lượng tích cực. Sự kết hợp này sẽ tạo ra sự hài hòa và hỗ trợ gia chủ trong việc đạt được thành công trong cuộc sống.\n\n";
    } else if (userElement === "Kim" && fishPoint <= 0.60) {
      comment += "\n\nVới gia chủ mang mệnh Kim, việc chọn cá Koi có màu trắng, bạc và vàng kim không chỉ làm tăng vận khí mà còn mang lại sự thịnh vượng và phát triển trong cuộc sống. Màu trắng là màu bản mệnh của Kim, đại diện cho sự tinh khiết và thanh thoát, trong khi màu bạc và vàng kim thể hiện sự giàu có và thành công. Bên cạnh đó, việc lựa chọn những chú cá Koi có màu vàng hoặc nâu từ mệnh Thổ cũng rất hữu ích, vì Thổ sinh Kim, giúp gia tăng tài lộc và sự thịnh vượng cho gia chủ. Sự kết hợp này sẽ tạo ra nguồn năng lượng tốt, giúp gia chủ mệnh Kim dễ dàng đạt được mục tiêu.\n\n";
    } 

    return comment; // Remove any trailing newlines
  }

  // Hàm hiển thị modal khi người dùng chọn cá
  const showFishDetails = async (fish) => {
    try {
     
      
      setSelectedFishDetail(fish);
      setIsModalVisible(true);
    } catch (error) {
      console.error("Error fetching fish details:", error);
      toast.error("Error fetching fish details");
    }
  };
  const showFishColor = async (fish) => {
    try {
      let fishColors;
      if (allFishColorWeights[fish.koiType]) {
        fishColors = allFishColorWeights[fish.koiType];
      } else {
        const response = await api.get(`TypeColor/GetAllTypeColor`);
        const allColors = response.data;
        fishColors = allColors.filter(color => color.koiType === fish.koiType);
      }
      setSelectedFishDetail(fish);
      setColorWeights(fishColors.map(color => ({
        colorId: color.colorId,
        percentage: color.percentage || 0
      })));
    } catch (error) {
      console.error("Error fetching fish details:", error);
      toast.error("Error fetching fish details");
    }
  };

  // Hàm xử lý thay đổi tỉ trọng màu
  const handleColorWeightChange = (koiType, index, value) => {
    setAllFishColors(prev => {
      const updatedColors = [...prev[koiType]];
      updatedColors[index] = {
        ...updatedColors[index],
        percentage: Math.max(0, Math.min(1, value))
      };
      setFishPoints(prev => {
          const newPoints = { ...prev };
          delete newPoints[koiType];
          return newPoints;
        });

      // Check if the fish is currently selected
      const isSelected = selectedFishes.some(fish => fish.koiType === koiType);
      if (isSelected) {
        // If it's selected, remove it from selectedFishes
        setSelectedFishes(prev => prev.filter(fish => fish.koiType !== koiType));
        // Also remove the fish point
        
      }

      return {
        ...prev,
        [koiType]: updatedColors
      };
    });
  };

  // Hàm kiểm tra tổng tỉ trọng phải là 100%
  const validateColorWeights = (koiType) => {
    const colors = allFishColors[koiType];
    const totalPercentage = colors.reduce((sum, color) => sum + color.percentage, 0);
    if (Math.abs(totalPercentage - 1) < 0.01) {
      return { valid: true, message: '' };
    } else if (totalPercentage > 1) {
      return { valid: false, message: 'Total percentage exceeds 100%' };
    } else {
      return { valid: false, message: 'Total percentage is less than 100%' };
    }
  };

 
  const handleResetColors = (koiType) => {
    const originalColors = allFishColors[koiType].map(color => ({
      ...color,
      percentage: color.originalPercentage || 0
    }));
    setAllFishColors(prev => ({
      ...prev,
      [koiType]: originalColors

    }));
    setFishPoints(prev => {
      const newPoints = { ...prev };
      delete newPoints[koiType];
      return newPoints;
    });
  };


  const handleSelectFish = async (fish) => {
    const validation = validateColorWeights(fish.koiType);
    if (validation.valid) {
      const selectedFishData = {
        koiType: fish.koiType,
        colors: allFishColors[fish.koiType]
      };
      
      try {
        // Get the DOB from the form
        const dob = form.getFieldValue('birthdate');
        const formattedDob = dob ? dob.format('YYYY-MM-DD') : '';
  
        // Prepare the payload for the API
        const payload = {
          koiType: fish.koiType,
          colors: allFishColors[fish.koiType].map(color => ({
            colorId: color.colorId,
            percentage: color.percentage
          }))
        };
  
        // Fetch fish point from API
        const response = await api.post('Compatibility/GetAttributeCustomColor', payload, {
          params: { dob: formattedDob }
        });
        const fishPoint = response.data;
        
        setFishPoints(prev => ({
          ...prev,
          [fish.koiType]: fishPoint
        }));
        
        setSelectedFishes(prev => {
          const existingIndex = prev.findIndex(f => f.koiType === fish.koiType);
          if (existingIndex !== -1) {
            // If fish is already selected, remove it
            const newSelected = prev.filter(f => f.koiType !== fish.koiType);
            form.setFieldsValue({ selectedFishes: newSelected });
            setFishPoints(prev => {
              const newPoints = { ...prev };
              delete newPoints[fish.koiType];
              return newPoints;
            });
            return newSelected;
          } else {
            // If fish is not selected, add it
            const newSelected = [...prev, selectedFishData];
            form.setFieldsValue({ selectedFishes: newSelected });
            return newSelected;
          }
        });
      } catch (error) {
        console.error("Error fetching fish point:", error);
        toast.error("Error fetching fish point");
      }
    } else {
      toast.error(validation.message);
    }
  };

  const handleRemoveFish = (fishToRemove) => {
    setSelectedFishes(prev => {
      const newSelected = prev.filter(fish => fish.koiType !== fishToRemove.koiType);
      form.setFieldsValue({ selectedFishes: newSelected });
      return newSelected;
    });
  };

  const handleSelectPondShape = async (shapeId) => {
    if (selectedPondShape === shapeId) {
      setSelectedPondShape(null);
      form.setFieldsValue({ selectedPondShape: null });
      setShapePoint(null);
    } else {
      setSelectedPondShape(shapeId);
      form.setFieldsValue({ selectedPondShape: shapeId });
      
      try {
        const dob = form.getFieldValue('birthdate');
        const formattedDob = dob ? dob.format('YYYY-MM-DD') : '';
        
        const response = await api.get(`Compatibility/GetPointOfShapeByShapeIDAndDOB`, {
          params: { 
            shapeId: shapeId,
            dob: formattedDob
          }
        });
        
        setShapePoint(response.data);
      } catch (error) {
        console.error("Error fetching shape point:", error);
        toast.error("Error fetching shape point");
      }
    }
  };

  const handleSelectDirection = async () => {
    try {
      const direction = form.getFieldValue('pondDirection');
      const dob = form.getFieldValue('birthdate');
      const gender = form.getFieldValue('Gender');

      if (!direction || !dob || !gender) {
        toast.error("Please select direction, date of birth, and gender");
        return;
      }

      const formattedDob = dob.format('YYYY-MM-DD');

      console.log('Sending request with:', { Direction: direction, DOB: formattedDob, Gender: gender });

      const response = await api.get(`Compatibility/GetPointOfDirectionByDirecDOBGEN`, {
        params: { 
          Direction: direction,
          DOB: formattedDob,
          Gender: gender
        }
      });
      
      console.log('Received response:', response.data);
      
      setDirectionPoint(response.data);
      
      form.setFieldsValue({ pondDirection: direction });
    } catch (error) {
      console.error("Error fetching direction point:", error);
      toast.error("Error fetching direction point");
    }
  };


  return (
    <>
      <HeaderTemplate />
      <div className="compute-container">
        <h1 className="page-title">Tính toán độ tương hợp</h1>
        <div className="compute-content">
          <div className="form-compute">
            <Form
              form={form}
              onFinish={onFinish}
              layout="vertical"
              className="compatibility-form"
            >
              <div className="birthdate-gender-row">
                <Form.Item
                  name="birthdate"
                  label="Ngày tháng năm sinh"
                  rules={[
                    { required: true, message: "Vui lòng chọn ngày sinh" },
                  ]}
                >
                  <DatePicker format="YYYY-MM-DD" style={{ width: "100%" }} />
                </Form.Item>
                <Form.Item
                  label="Giới tính"
                  name="Gender"
                  rules={[
                    { required: true, message: "Hãy chọn giới tính của bạn!" },
                  ]}
                >
                  <Radio.Group>
                    <Radio value="male">Nam</Radio>
                    <Radio value="female">Nữ</Radio>
                  </Radio.Group>
                </Form.Item>
              </div>

              <Form.Item
                label="Chọn loại cá"
                name="selectedFishes"
                rules={[{ required: false, message: "Vui lòng chọn ít nhất một loại cá" }]}

              >
                <div className="filter-section">
                  <Form layout="inline">
                    <Form.Item label="Bảng mệnh">
                      <Select value={elementFilter} onChange={setElementFilter}>
                        <Option value="all">Tất cả</Option>
                        {elements.map((element) => (
                          <Option
                            key={element.elementId}
                            value={element.elementId}
                          >
                            {element.elementId}
                          </Option>
                        ))}
                      </Select>
                    </Form.Item>
                    <Form.Item label="Màu sắc">
                      <Select value={colorFilter} onChange={setColorFilter}>
                        <Option value="all">Tất cả</Option>
                        {colors.map((color) => (
                          <Option key={color.colorId} value={color.colorId}>
                            {color.colorId}
                          </Option>
                        ))}
                      </Select>
                    </Form.Item>

                    <Form.Item label="Tên giống cá">
                      <Input
                        placeholder="Tìm kiếm theo tên giống cá"
                        onChange={(e) => {
                          const searchTerm = e.target.value.toLowerCase();
                          const filtered = fishList.filter((fish) =>
                            fish.koiType.toLowerCase().includes(searchTerm)
                          );
                          setFilteredFishList(filtered);
                        }}
                      />
                    </Form.Item>
                  </Form>
                </div>

                
                <div  className="fish-list">
                  {filteredFishList.map((fish) => (
                    <div 
                      key={fish.koiType} 
                      className={`fish-card ${selectedFishes.some(f => f.koiType === fish.koiType) ? 'selected' : ''}`}
                    >
                      <div className="fish-card-left">
                        <div>
                          <img onClick={() => showFishDetails(fish)} src={`/koi_image/${fish.image}`} alt={fish.image} />
                          <p>{fish.koiType}</p>
                          
                        </div>
                      </div>
                      <div className="fish-card-right">
  {allFishColors[fish.koiType] && (
    <>
      <div style={{ minHeight: "100%", maxHeight: "100%" }}>
        <h3>Điều chỉnh tỉ trọng màu</h3>
        {allFishColors[fish.koiType].map((color, index) => (
          <div key={color.colorId} style={{ marginBottom: "16px" }}>
            <span>{color.colorId}: </span>
            <Slider
              min={0}
              max={1}
              step={0.01}
              value={color.percentage}
              onChange={(value) => handleColorWeightChange(fish.koiType, index, value)}
            />
            <InputNumber
              min={0}
              max={1}
              step={0.01}
              value={color.percentage}
              onChange={(value) => handleColorWeightChange(fish.koiType, index, value)}
            />
          </div>
        ))}
        {!validateColorWeights(fish.koiType).valid && (
          <p style={{ color: 'red' }}>{validateColorWeights(fish.koiType).message}</p>
        )}
        {fishPoints[fish.koiType] !== undefined && (
  <p className="fish-point">Độ tương hợp: {(fishPoints[fish.koiType] * 100).toFixed(2)}%</p>
)}
      </div>
      <div className="fish-card-buttons">
        <Button 
          type="primary" 
          onClick={() => handleSelectFish(fish)}
          disabled={!validateColorWeights(fish.koiType).valid}
        >
          {selectedFishes.some(f => f.koiType === fish.koiType) ? 'Xóa cá' : 'Chọn cá'}
        </Button>
        <Button onClick={() => handleResetColors(fish.koiType)}>
          Đặt lại màu
        </Button>
      </div>
    </>
  )}
</div>

                    </div>
                  ))}
                </div>
              </Form.Item>


              {/* Hiển thị modal trong phần JSX */}
              <Modal
               
                visible={isModalVisible}
                // onOk={handleModalOk}
                onCancel={() => setIsModalVisible(false)}
                footer={[
                  
                ]}
              >
                {selectedFishDetail && (
                  <div className="fish-detail-modal">
                     <img className="fish-detail-image"
                      src={`/koi_image/${selectedFishDetail.image}`}
                      alt={selectedFishDetail.koiType}
                    />
                    <div className="fish-detail-info">
                      <h1>{selectedFishDetail.koiType}</h1>
                   
                      <p>
                       <strong>Bản mệnh:</strong> {selectedFishDetail.element}
                      </p>
                      <p>
                        <strong>Giới thiệu:</strong>{" "}
                          {selectedFishDetail.description}
                      </p>
                    </div>
                    
                  </div>
                )}
              </Modal>

              {/* Add the pond shape selection here */}
              <Form.Item
                name="selectedPondShape"
                label="Hình dáng hồ"
                rules={[

                  { required: false, message: "Vui lòng chọn hình dáng hồ" },

                ]}
              >
                <div className="pond-shape-list">
                  {pondShapes.map((shape) => (
                    <div
                      key={shape.shapeId}
                      className={`pond-shape-card ${
                        selectedPondShape === shape.shapeId ? "selected" : ""
                      }`}

                      onClick={() => handleSelectPondShape(shape.shapeId)}
                    >
                      <img src={`/pond/${shape.image}`} alt={shape.shapeId} />
                      <div className="pond-shape-info">
                      <p>{shape.shapeId}</p>
                      {selectedPondShape === shape.shapeId && shapePoint !== null && (
                        <p className="shape-point">Điểm tương hợp: {(shapePoint ).toFixed(2)}%</p>
                      )}
                    </div>

                    </div>
                  ))}
                </div>
              </Form.Item>

              <div className="pond-direction-select">
              <Form.Item
                className="direction-select"
                name="pondDirection"
                label="Hướng đặt hồ"
                rules={[
                  { required: false, message: "Vui lòng chọn hướng" },
                ]}
              >
                <Select  placeholder="Vui lòng chọn hướng đặt hồ" onChange={handleSelectDirection}>
                  <Option value="">Chọn hướng đặt hồ</Option>
                  {pondDirections.map((direction) => (
                    <Option key={direction.directionId} value={direction.directionId}>

                      {direction.directionId}
                    </Option>
                  ))}
                </Select>

               
              </Form.Item>
              {directionPoint !== null && (
                <div className="direction-point">
                  Điểm hướng: {(directionPoint ).toFixed(2)}%
                </div>
              )}
              </div>
              

              <Form.Item>
                <Button type="primary" htmlType="submit">
                  Tính toán
                </Button>
              </Form.Item>
            </Form>
            {compatibilityResult && (
              <div className="result-section">
                <h2 className="result-title">Kết quả tính toán</h2>

                <p className="result-score">
                  Độ tương hợp: {compatibilityResult}%
                </p>
                <p className="result-comment">
                  Nhận xét: {getCompatibilityComment(compatibilityResult)}
                </p>
              </div>
            )}
          </div>
        </div>
      </div>
      <FooterPage />
    </>
  );
}


export default ComputeCompability;

