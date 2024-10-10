Feature('KoiVarietyAPI');

Scenario('Get list of koi varieties by DOB', async ({ I }) => {
  const dob = '1990-01-01'; // Ngày sinh ví dụ
  const response = await I.sendGetRequest(`/api/KoiVariety/GetListKoiByDOBOrder?dob=${dob}`);
  
  I.seeResponseCodeIs(200);

  // Kiểm tra từng phần tử
  I.seeResponseContainsJson({
    koiType: "Beni Kumonryu",
    element: "Hỏa",
    description: "Koi Beni Kumonryu là một biến thể hiếm hoi của Kumonryu, chúng thường có ba màu là đỏ, đen và trắng.",
    typeColors: []
  });

  I.seeResponseContainsJson({
    koiType: "Gin Rin Yamato Nishiki",
    element: "Hỏa",
    description: "Gin Rin Yamato nishiki koi là loài cá được lai tạo khoảng thời gian sau này của giống koi sanke. Có thể nói cá koi yamato nishiki giúp vẻ đẹp màu sắc Gin Rin tiến đến một tầm mới hơn khi phủ lên mình cá một lớp ánh kim loại lấp lánh.",
    typeColors: []
  });
});

Scenario('Get Quantity of fish by DOB', async ({ I }) => {
    const dob = '1990-01-01'; // Ngày sinh ví dụ
    const response = await I.sendGetRequest(`/api/KoiVariety/GetQuantityByDOB?dob=${dob}`);
    
    I.seeResponseCodeIs(200); // Kiểm tra mã phản hồi là 200
    I.seeResponseContainsKeys(['elementId', 'description', 'element']);
});

  Scenario('Get list of koi varieties by Color', async ({ I }) => {
    const color = 'Đỏ'; 
    const response = await I.sendGetRequest(`/api/KoiVariety/GetListKoiByColor?color=${color}`);
    
    I.seeResponseCodeIs(200);
  
    // Kiểm tra từng phần tử
    I.seeResponseContainsJson({
      koiType: "Asagi",
      element: "Thủy",
      description: "Asagi chính là giống sản sinh ra Nishikigoi, chúng bắt nguồn từ loài cá chép đen thường sinh sống ở những vùng sông hoặc suối, những chú cá chép đen có sự tiến hóa vượt bậc và được người dân Nhật chọn lựa những chú cá có màu sắc như Trắng, Xanh dương và màu đỏ và giữ lại trong hồ.",
      typeColors: []
    });
  
    I.seeResponseContainsJson({
      koiType: "Beni Kumonryu",
      element: "Hỏa",
      description: "Koi Beni Kumonryu là một biến thể hiếm hoi của Kumonryu, chúng thường có ba màu là đỏ, đen và trắng.",
      typeColors: []
    });
  });

  Scenario('Get All Koi', async ({ I }) => {
    const response = await I.sendGetRequest(`/api/KoiVariety/GetAllKoi`);
    I.seeResponseCodeIs(200);
    // Kiểm tra từng phần tử
    I.seeResponseContainsJson({
      koiType: "Asagi",
      element: "Thủy",
      description: "Asagi chính là giống sản sinh ra Nishikigoi, chúng bắt nguồn từ loài cá chép đen thường sinh sống ở những vùng sông hoặc suối, những chú cá chép đen có sự tiến hóa vượt bậc và được người dân Nhật chọn lựa những chú cá có màu sắc như Trắng, Xanh dương và màu đỏ và giữ lại trong hồ.",
      typeColors: []
    });
    I.seeResponseContainsJson({
      koiType: "Beni Kumonryu",
      element: "Hỏa",
      description: "Koi Beni Kumonryu là một biến thể hiếm hoi của Kumonryu, chúng thường có ba màu là đỏ, đen và trắng.",
      typeColors: []
    });
  });