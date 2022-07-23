class Node:
    def __init__(self,data,level,fval):
        "" "Khởi tạo nút với dữ liệu, mức của nút và giá trị được tính toán" ""
        self.data = data
        self.level = level
        self.fval = fval
    def generate_child(self):
        """Tạo các nút con từ nút đã cho bằng cách di chuyển khoảng trống"""
        "" "theo bốn hướng {lên, xuống, trái, phải} """
        x,y = self.find(self.data,'_')
        "" "val_list chứa các giá trị vị trí để di chuyển khoảng trống ở một trong hai"""
        "" "4 hướng [lên, xuống, trái, phải] tương ứng. "" "
        val_list = [[x,y-1],[x,y+1],[x-1,y],[x+1,y]]
        children = []
        for i in val_list:
            child = self.shuffle(self.data,x,y,i[0],i[1])
            if child is not None:
                child_node = Node(child,self.level+1,0)
                children.append(child_node)
        return children    
    def shuffle(self,puz,x1,y1,x2,y2):
        "" "Di chuyển khoảng trống theo hướng đã cho và nếu giá trị vị trí nằm ngoài"""
        "" " giới hạn trả về là Không """
        if x2 >= 0 and x2 < len(self.data) and y2 >= 0 and y2 < len(self.data):
            temp_puz = []
            temp_puz = self.copy(puz)
            temp = temp_puz[x2][y2]
            temp_puz[x2][y2] = temp_puz[x1][y1]
            temp_puz[x1][y1] = temp
            return temp_puz
        else:
            return None      
    def copy(self,root):
        """ Copy function to create a similar matrix of the given node"""
        temp = []
        for i in root:
            t = []
            for j in i:
                t.append(j)
            temp.append(t)
        return temp             
    def find(self,puz,x):
        "" "Cụ thể được sử dụng để tìm vị trí của khoảng trống" ""
        for i in range(0,len(self.data)):
            for j in range(0,len(self.data)):
                if puz[i][j] == x:
                    return i,j
class Puzzle:
    def __init__(self,size):
        "" "Khởi tạo kích thước câu đố theo kích thước đã chỉ định, danh sách mở và đóng thành trống" ""
        self.n = size
        self.open = []
        self.closed = []
    def accept(self):
        "" "Chấp nhận câu đố từ người dùng" ""
        puz = []
        for i in range(0,self.n):
            temp = input().split(" ")
            puz.append(temp)
        return puz
    def f(self,start,goal):
        "" "Hàm Heuristic để tính toán giá trị màu f (x) = h (x) + g (x)" ""
        return self.h(start.data,goal)+start.level
    def h(self,start,goal):
        "" "Tính hiệu số giữa các câu đố đã cho" ""
        temp = 0
        for i in range(0,self.n):
            for j in range(0,self.n):
                if start[i][j] != goal[i][j] and start[i][j] != '_':
                    temp += 1
        return temp
    def process(self):
        "" "Chấp nhận trạng thái Câu đố bắt đầu và Mục tiêu" ""
        print("Nhập ma trận trạng thái bắt đầu \n")
        start = self.accept()
        print("Nhập ma trận trạng thái mục tiêu \n")        
        goal = self.accept()
        start = Node(start,0,0)
        start.fval = self.f(start,goal)
        "" "Đặt nút bắt đầu trong danh sách mở" ""
        self.open.append(start)
        print("\n\n")
        while True:
            cur = self.open[0]
            print("")
            print("  | ")
            print("  | ")
            print(" \\\'/ \n")
            for i in cur.data:
                for j in i:
                    print(j,end=" ")
                print("")
            "" "Nếu sự khác biệt giữa nút hiện tại và nút mục tiêu là 0, chúng tôi đã đạt đến nút mục tiêu" ""
            if(self.h(cur.data,goal) == 0):
                break
            for i in cur.generate_child():
                i.fval = self.f(i,goal)
                self.open.append(i)
            self.closed.append(cur)
            del self.open[0]
            "" "sắp xếp một danh sách dựa trên giá trị f" ""
            self.open.sort(key = lambda x:x.fval,reverse=False)
puz = Puzzle(3)
puz.process()
