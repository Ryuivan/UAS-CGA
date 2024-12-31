# UAS-CGA
### Fruit Hoops
Game ini merupakan permainan lempar buah yang menyerupai permainan basketball arcade dengan durasi ± 60 detik. Setiap buah yang berhasil masuk ke dalam ring akan menambahkan skor dan durasi waktu permainan. Penambahan skor dan durasi waktu bergantung pada berat buah, sehingga semakin berat buah yang dilempar, semakin banyak waktu yang ditambahkan. Rasio layar pada game disarankan untuk diatur pada rasio 16:9 guna memastikan pengalaman bermain yang optimal. 

### Fitur aplikasi:
Permainan ini dirancang dengan fitur utama berupa mekanisme melempar buah ke dalam ring. Pemain dapat menarik buah ke arah ring dan melepaskannya untuk melakukan lemparan.	 Setiap buah yang dilempar akan muncul secara acak di area permainan (dispawn secara random), memberikan variasi tantangan bagi pemain. Selain itu, kekuatan lemparan buah ditentukan oleh tarikan trajectory (by mouse), yang mempengaruhi arah dan kecepatan lemparan. Sistem juga menyediakan bantuan berupa setengah lintasan (half trajectory), yang menunjukkan sebagian dari jalur lemparan buah, mempermudah pemain dalam menentukan arah dan kekuatan lemparan untuk meningkatkan akurasi dalam memasukkan buah ke dalam ring.

Dengan adanya variasi berat pada setiap buah, pemain dihadapkan pada tantangan yang semakin meningkat. Dalam hal ini, strategi dan ketepatan lemparan menjadi kunci untuk mencapai skor tertinggi. Selain itu, pemain diharuskan beradaptasi dengan perubahan pola pergerakan ring yang terjadi pada mode menengah dan sulit. Dengan pergerakan ring yang semakin kompleks, pemain ditantang untuk meningkatkan keterampilan dalam mengarahkan lemparan.

Berat setiap buah diatur menggunakan properti "Mass" pada komponen Rigidbody di Unity. Dalam permainan ini, buah, lapangan beserta hoops and wall memiliki collider yang memungkinkan interaksi antara objek dalam game. Terdapat trigger pada ring yang mendeteksi saat buah berhasil masuk ke dalam ring dan secara otomatis menambah skor pemain. Kemudian, setiap buah yang sudah dilempar melebihi ketinggian tertentu akan di-despawn setelah 5 detik, demi menjaga performa permainan.

Berikut adalah penjelasan berat buah, skor yang didapat dan waktu yang ditambahkan ketika buah masuk ke ring:
1.	Apel:
    -	Skor	: 22.
    -	Berat : 2.
    -	Waktu	: 3 detik.
2.	Pisang:
    - Skor	: 15.
    - Berat	: 1.
    - Waktu	: 3 detik.
3.	Jeruk:
    - Skor	: 20.
    - Berat : 1.5.
    - Waktu	: 3 detik.
4.	Pir:
    - Skor	: 25.
    - Berat	: 1.8.
    - Waktu	: 3 detik.
5.	Nanas:
    - Skor	: 35.
    - Berat : 3.
    - Waktu	: 5 detik.
      
Hasil skor dari permainan akan disimpan ke sebuah file dan akan digunakan sebagai perbandingan untuk permainan selanjutnya (highest score). Pemain dapat melihat skor terbaik yang pernah dicapai dan berusaha untuk mengalahkan rekor tersebut di setiap sesi akhir permainan. Selain sistem skor, terdapat 3 mode tantangan pada permainan. Berikut adalah penjelasan terhadap 3 mode tantangan:
1.	Ring yang tidak bergerak (mudah)
Pada mode ini, ring tetap diam di tengah lapangan. Mode ini berlaku selama sisa waktu permainan masih di atas 40 detik.
2.	Ring bergerak secara horizontal (menengah)
Pada mode ini, ring dapat bergerak ke kiri dan kanan dengan kecepatan yang bervariasi. Mode ini berlaku ketika sisa waktu berada di antara 20 hingga 40 detik.
3.	Ring bergerak secara horizontal, depan belakang serta membesar mengecil(sulit)
Pada mode ini, ring bergerak lebih kompleks dengan pola ke kiri-kanan, depan-belakang, serta ukurannya yang membesar dan mengecil. Mode ini berlaku ketika sisa waktu kurang dari 20 detik.


Anggota kelompok:
1. Hans Philemon
2. Khaleb Andhyka Aprijadi
3. Vianca Vanesia Barhan
4. Ryu Ivan Wijaya
5. Kenta
