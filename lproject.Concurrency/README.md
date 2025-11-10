**Multithreading** là mô hình cho phép một tiến trình (process) có nhiều luồng thực thi (threads) hoạt động đồng thời.

Trong hệ thống đơn nhân (single-core), CPU có thể **chuyển đổi nhanh** giữa các thread để xử lý những công việc khác nhau, khiến ta *cảm giác* như chúng đang chạy song song.

Khi có nhiều nhân CPU hơn (multi-core), mỗi core có thể xử lý một thread độc lập, giúp nhiều thread **thực sự chạy đồng thời** (parallel execution).

Mỗi thread tồn tại với những trạng thái nhất định như **Running**, **Ready**, **Blocked**, hoặc **Terminated**.

Tại bất kỳ thời điểm nào, **CPU core** sẽ chọn một thread ở trạng thái **Ready** để thực thi (chuyển sang **Running**).

Thread đó sẽ tiếp tục chạy cho đến khi hoàn thành hoặc khi gặp một tình huống khiến nó phải **tạm dừng** (Blocked) — chẳng hạn như đọc/ghi file, chờ dữ liệu từ mạng, hoặc thao tác I/O nói chung.

Những thao tác khiến thread tạm dừng được gọi là **blocking operations**.

Khi thread bị block, nó sẽ “ngủ” cho đến khi hệ điều hành thông báo rằng tác vụ bên ngoài (I/O, timer, network...) đã hoàn thành.

Mỗi thread có **vùng nhớ cục bộ (local stack)** riêng, và việc chuyển đổi qua lại giữa các thread (**context switching**) đều tốn chi phí CPU.

Nếu chương trình có nhiều blocking operation mà mỗi operation lại chiếm giữ một thread riêng, rất nhiều thread sẽ ở trạng thái idle — không thực thi nhưng vẫn chiếm tài nguyên.

Trong trường hợp có hàng trăm hoặc hàng nghìn tác vụ I/O cùng chờ, hệ thống có thể phải tạo ra hàng nghìn thread → gây quá tải, tiêu tốn RAM và CPU → ứng dụng có thể **chậm hoặc crash**.

Để giải quyết vấn đề này, **asynchronous programming** ra đời như một cơ chế **giải phóng thread** khỏi việc chờ đợi.

Khi gặp một blocking operation, thread không cần ngồi chờ mà có thể **trả lại quyền điều khiển** cho hệ điều hành, để thực hiện công việc khác.

Khi operation hoàn tất, hệ điều hành (hoặc runtime) **phát tín hiệu (event)** báo cho chương trình biết, và một thread khả dụng (thường từ thread pool) sẽ **xử lý phần tiếp theo (continuation)** của tác vụ đó.

Nhờ cơ chế này, chương trình có thể **kết hợp lợi ích của multithreading và async programming**:

- Multithreading giúp **chạy nhiều tác vụ logic cùng lúc** (CPU-bound hoặc I/O-bound).
- Async programming giúp **giảm lãng phí tài nguyên** bằng cách không để thread bị kẹt ở các tác vụ I/O.

Trong thực tế, khi một blocking operation đang diễn ra, chương trình (nếu không phụ thuộc vào kết quả của operation đó) vẫn có thể **tiếp tục thực thi các phần việc khác**, và chỉ tạm dừng lại khi thật sự cần kết quả của operation — điều này mang lại hiệu năng cao, khả năng phản hồi nhanh, và khả năng mở rộng tốt hơn.

## Parallel Execution

### Thread

Khi một proces start nó sẽ bắt đầu với một main thread run trên Main method (có thể có một số thread system bên cạnh nó)

Có thể tạo thêm thread bổ sung để thực hiện các công việc mong muốn, trong C#, class Thread đại diện cho một thread được sử dụng bên dưới:

```csharp
var thread = new Thread(delegate) 
// Delegate là một function void có một tham số object? hoặc không có.
thread.Start(object)
// Truyền tham số cho delegate.
```

Việc construct/deconstruct thread object sử dụng tương đối lượng tài nguyên nhạy cảm, vì vậy việc tạo và huỷ chúng nhiều cũng có thể dẫn tới phần lớn thời gian nó dùng để quản lí việc khỏi tạo/huỷ bỏ thay vì thực hiện công việc được gán trong delegate.

Vì vậy lí tưởng nhất sử dụng Thread.Start cho các long-running tasks, bên cạnh đó không dùng cho async/await, short tasks.

### Thread pool

Thread pool là nơi chứa một số thread được khởi tạo sẵn mục đích sử dụng cho các short-running tasks. Các short-running tasks thay phiên nhau sử dụng thread từ thread pool mà không có thêm constructor/deconstructor thread object nào.

```csharp
ThreadPool.QueueUserWorkItem(delegate, object? paramOfDelegate); 
```

### Task.Run

Task.Run(task) cũng sử dụng ThreadPool nhằm mục đích hạn chế việc cấp phát/giải phóng Thread object. Bởi vì các Task cũng là các short-running tasks, vì phần lớn workload của chúng thực hiện bởi các component outside CPU.

```csharp
Task.Run(Task)
Task.Run(() => Task)
```