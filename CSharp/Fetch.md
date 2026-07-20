<!DOCTYPE html>
<html>
<head>
	<title>A - Random Sum Game</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<meta http-equiv="Content-Language" content="ja">
	<meta name="viewport" content="width=device-width,initial-scale=1.0">
	<meta name="format-detection" content="telephone=no">
	<meta name="google-site-verification" content="nXGC_JxO0yoP1qBzMnYD_xgufO6leSLw1kyNo2HZltM" />

	
	<script async src="https://www.googletagmanager.com/gtag/js?id=G-RC512FD18N"></script>
	<script>
		window.dataLayer = window.dataLayer || [];
		function gtag(){dataLayer.push(arguments);}
		gtag('js', new Date());
		gtag('set', 'user_properties', {
			
				'login_status': 'logged_in',
			
		});
		gtag('config', 'G-RC512FD18N');
	</script>

	
	<meta name="description" content="プログラミング初級者から上級者まで楽しめる、競技プログラミングコンテストサイト「AtCoder」。オンラインで毎週開催プログラミングコンテストを開催しています。競技プログラミングを用いて、客観的に自分のスキルを計ることのできるサービスです。">
	<meta name="author" content="AtCoder Inc.">

	<meta property="og:site_name" content="AtCoder">
	
	<meta property="og:title" content="A - Random Sum Game" />
	<meta property="og:description" content="プログラミング初級者から上級者まで楽しめる、競技プログラミングコンテストサイト「AtCoder」。オンラインで毎週開催プログラミングコンテストを開催しています。競技プログラミングを用いて、客観的に自分のスキルを計ることのできるサービスです。" />
	<meta property="og:type" content="website" />
	<meta property="og:url" content="https://atcoder.jp/contests/ahc053/tasks/ahc053_a" />
	<meta property="og:image" content="https://img.atcoder.jp/assets/atcoder.png" />
	<meta name="twitter:card" content="summary" />
	<meta name="twitter:site" content="@atcoder" />
	
	<meta property="twitter:title" content="A - Random Sum Game" />

	<link href="//fonts.googleapis.com/css?family=Lato:400,700" rel="stylesheet" type="text/css">
	<link rel="stylesheet" type="text/css" href="//img.atcoder.jp/public/afbfd62/css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="//img.atcoder.jp/public/afbfd62/css/base.css">
	<link rel="shortcut icon" type="image/png" href="//img.atcoder.jp/assets/favicon.png">
	<link rel="apple-touch-icon" href="//img.atcoder.jp/assets/atcoder.png">
	<script src="//img.atcoder.jp/public/afbfd62/js/lib/jquery-1.9.1.min.js"></script>
	<script src="//img.atcoder.jp/public/afbfd62/js/lib/bootstrap.min.js"></script>
	<script src="//img.atcoder.jp/public/afbfd62/js/cdn/js.cookie.min.js"></script>
	<script src="//img.atcoder.jp/public/afbfd62/js/cdn/moment.min.js"></script>
	<script src="//img.atcoder.jp/public/afbfd62/js/cdn/moment_js-ja.js"></script>
	<script>
		var LANG = "ja";
		var userScreenName = "Coji";
		var csrfToken = "3dL3gwWP0TO1UYLmPM5t8gXd79YLsVlgYmVS/1+Aax0="
	</script>
	<script src="//img.atcoder.jp/public/afbfd62/js/utils.js"></script>
	
	
		<script src="//img.atcoder.jp/public/afbfd62/js/contest.js"></script>
		<link href="//img.atcoder.jp/public/afbfd62/css/contest.css" rel="stylesheet" />
		<script>
			var contestScreenName = "ahc053";
			var remainingText = "残り時間";
			var countDownText = "開始まであと";
			var startTime = moment("2025-09-13T19:00:00+09:00");
			var endTime = moment("2025-09-13T23:00:00+09:00");
		</script>
		<style></style>
	
	
		<link href="//img.atcoder.jp/public/afbfd62/css/cdn/select2.min.css" rel="stylesheet" />
		<link href="//img.atcoder.jp/public/afbfd62/css/cdn/select2-bootstrap.min.css" rel="stylesheet" />
		<script src="//img.atcoder.jp/public/afbfd62/js/lib/select2.min.js"></script>
	
	
		<script src="//img.atcoder.jp/public/afbfd62/js/ace/ace.js"></script>
		<script src="//img.atcoder.jp/public/afbfd62/js/ace/ext-language_tools.js"></script>
	
	
		<script src="//img.atcoder.jp/public/afbfd62/js/cdn/run_prettify.js"></script>
	
	
		<link rel="stylesheet" href="//img.atcoder.jp/public/afbfd62/css/cdn/katex.min.css">
		<script defer src="//img.atcoder.jp/public/afbfd62/js/cdn/katex.min.js"></script>
		<script defer src="//img.atcoder.jp/public/afbfd62/js/cdn/auto-render.min.js"></script>
		<script>$(function(){$('var').each(function(){var html=$(this).html().replace(/<sub>/g,'_{').replace(/<\/sub>/g,'}');$(this).html('\\('+html+'\\)');});});</script>
		<script>
			var katexOptions = {
				delimiters: [
					{left: "$$", right: "$$", display: true},
					
					{left: "\\(", right: "\\)", display: false},
					{left: "\\[", right: "\\]", display: true}
				],
      	ignoredTags: ["script", "noscript", "style", "textarea", "code", "option"],
				ignoredClasses: ["prettyprint", "source-code-for-copy"],
				throwOnError: false
			};
			document.addEventListener("DOMContentLoaded", function() { renderMathInElement(document.body, katexOptions);});
		</script>
	
	
	
	
	
	
	
	
	
	<script src="//img.atcoder.jp/public/afbfd62/js/base.js"></script>
</head>

<body>

<script type="text/javascript">
	var __pParams = __pParams || [];
	__pParams.push({client_id: '468', c_1: 'atcodercontest', c_2: 'ClientSite'});
</script>
<script type="text/javascript" src="https://cdn.d2-apps.net/js/tr.js" async></script>


<div id="modal-contest-start" class="modal fade" tabindex="-1" role="dialog">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title">コンテスト開始</h4>
			</div>
			<div class="modal-body">
				<p>第12回 Asprova プログラミングコンテスト（AtCoder Heuristic Contest 053）が開始されました。</p>
			</div>
			<div class="modal-footer">
				
					<a class="btn btn-primary" href='/contests/ahc053/tasks'>問題一覧ページへ移動</a>
				
			</div>
		</div>
	</div>
</div>
<div id="modal-contest-end" class="modal fade" tabindex="-1" role="dialog">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title">コンテスト終了</h4>
			</div>
			<div class="modal-body">
				<p>第12回 Asprova プログラミングコンテスト（AtCoder Heuristic Contest 053）は終了しました。</p>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">閉じる</button>
			</div>
		</div>
	</div>
</div>
<div id="main-div" class="float-container">


	<nav class="navbar navbar-inverse navbar-fixed-top">
		<div class="container-fluid">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse" aria-expanded="false">
					<span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
				</button>
				<a class="navbar-brand" href="/home"></a>
			</div>
			<div class="collapse navbar-collapse" id="navbar-collapse">
				<ul class="nav navbar-nav">
				
					<li><a class="contest-title" href="/contests/ahc053">第12回 Asprova プログラミングコンテスト（AtCoder Heuristic Contest 053）</a></li>
				
				</ul>
				<ul class="nav navbar-nav navbar-right">
					
					<li class="dropdown">
						<a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
							<img src='//img.atcoder.jp/assets/top/img/flag-lang/ja.png'> 日本語 <span class="caret"></span>
						</a>
						<ul class="dropdown-menu">
							<li><a href="/contests/ahc053/tasks/ahc053_a?lang=ja"><img src='//img.atcoder.jp/assets/top/img/flag-lang/ja.png'> 日本語</a></li>
							<li><a href="/contests/ahc053/tasks/ahc053_a?lang=en"><img src='//img.atcoder.jp/assets/top/img/flag-lang/en.png'> English</a></li>
						</ul>
					</li>
					
					
						<li class="dropdown">
							<a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
								<span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Coji (Contestant) <span class="caret"></span>
							</a>
							<ul class="dropdown-menu">
								<li><a href="/users/Coji"><span class="glyphicon glyphicon-user" aria-hidden="true"></span> マイプロフィール</a></li>
								<li class="divider"></li>
								<li><a href="/settings"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span> 基本設定</a></li>
								<li><a href="/settings/icon"><span class="glyphicon glyphicon-picture" aria-hidden="true"></span> アイコン設定</a></li>
								<li><a href="/settings/password"><span class="glyphicon glyphicon-lock" aria-hidden="true"></span> パスワードの変更</a></li>
								<li><a href="/settings/fav"><span class="glyphicon glyphicon-star" aria-hidden="true"></span> お気に入り管理</a></li>
								
								
								
								<li class="divider"></li>
								<li><a href="javascript:void(form_logout.submit())"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span> ログアウト</a></li>
							</ul>
						</li>
					
				</ul>
			</div>
		</div>
	</nav>

	<form method="POST" name="form_logout" action="/logout?continue=https%3A%2F%2Fatcoder.jp%2Fcontests%2Fahc053%2Ftasks%2Fahc053_a">
		<input type="hidden" name="csrf_token" value="3dL3gwWP0TO1UYLmPM5t8gXd79YLsVlgYmVS/1&#43;Aax0=" />
	</form>
	<div id="main-container" class="container"
		 	style="padding-top:50px;">
		


<div class="row">
	<div id="contest-nav-tabs" class="col-sm-12 mb-2 cnvtb-fixed">
	<div>
		<small class="contest-duration">
			
				コンテスト時間:
				<a href='http://www.timeanddate.com/worldclock/fixedtime.html?iso=20250913T1900&p1=248' target='blank'><time class='fixtime fixtime-full'>2025-09-13 19:00:00+0900</time></a> ~ <a href='http://www.timeanddate.com/worldclock/fixedtime.html?iso=20250913T2300&p1=248' target='blank'><time class='fixtime fixtime-full'>2025-09-13 23:00:00+0900</time></a> 
				(240分)
			
		</small>
		
	</div>
	<ul class="nav nav-tabs">
		<li><a href="/contests/ahc053"><span class="glyphicon glyphicon-home" aria-hidden="true"></span> トップ</a></li>
		
			<li class="active"><a href="/contests/ahc053/tasks"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span> 問題</a></li>
		

		
			<li><a href="/contests/ahc053/clarifications"><span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span> 質問 <span id="clar-badge" class="badge" ></span></a></li>
		

		
			<li><a href="/contests/ahc053/submit?taskScreenName=ahc053_a"><span class="glyphicon glyphicon-send" aria-hidden="true"></span> 提出</a></li>
		

		
			<li>
				<a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false"><span class="glyphicon glyphicon-list" aria-hidden="true"></span> 提出結果<span class="caret"></span></a>
				<ul class="dropdown-menu">
					
					
						<li><a href="/contests/ahc053/submissions/me"><span class="glyphicon glyphicon-user" aria-hidden="true"></span> 自分の提出</a></li>
						
						
					
				</ul>
			</li>
		

		
			
				
					<li><a href="/contests/ahc053/standings"><span class="glyphicon glyphicon-sort-by-attributes-alt" aria-hidden="true"></span> 順位表</a></li>
				
			
		

		
			<li><a href="/contests/ahc053/custom_test"><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span> コードテスト</a></li>
		

		
		

		<li class="pull-right"><a id="fix-cnvtb" href="javascript:void(0)"><span class="glyphicon glyphicon-pushpin" aria-hidden="true"></span></a></li>
	</ul>
</div>
	<div class="col-sm-12">
		<span class="h2">
			A - Random Sum Game
			
		</span>
		<span id="task-lang-btn" class="pull-right"><span data-lang="ja"><img src='//img.atcoder.jp/assets/top/img/flag-lang/ja.png'></span> / <span data-lang="en"><img src='//img.atcoder.jp/assets/top/img/flag-lang/en.png'></span></span>
		<script>
			$(function() {
				var ts = $('#task-statement span.lang');
				if (ts.children('span').size() <= 1) {
					$('#task-lang-btn').hide();
					ts.children('span').show();
					return;
				}

				var REMEMBER_LB = 5;
				var LS_KEY = 'task_lang';
				var taskLang = getLS(LS_KEY) || '';
				function isTaskLangSet(taskLang) { return taskLang === 'ja' || taskLang === 'en';}
				if (isTaskLangSet(taskLang)) {
					const params = new URLSearchParams(window.location.search);
					if (params.get('lang')) {
						setLS(LS_KEY, REMEMBER_LB);
						taskLang = LANG;
					}
				} else {
					taskLang = LANG;
				}
				ts.children('span.lang-' + taskLang).show();

				$('#task-lang-btn span').click(function() {
					var l = $(this).data('lang');
					ts.children('span').hide();
					ts.children('span.lang-' + l).show();

					taskLang = getLS(LS_KEY) || '';
					let changeTimes = 0;
					if (isTaskLangSet(taskLang)) {
						changeTimes = REMEMBER_LB;
					} else {
						changeTimes = parseInt(taskLang, 10);
						if (isNaN(changeTimes)) changeTimes = 0;
						changeTimes++;
					}
					if (changeTimes < REMEMBER_LB) setLS(LS_KEY, changeTimes);
					else setLS(LS_KEY, l);
				});
			});
		</script>
		<hr/>
		<p>
			実行時間制限: 2 sec / メモリ制限: 1024 MiB
			
			
		</p>

		<div id="task-statement">
			


	
	

			<span class="lang">
<span class="lang-ja">

<div class="part">
<section>
<h3>問題文</h3><p><var>N</var> 枚の何も書かれていないカードと、<var>L</var> 以上 <var>U</var> 以下の整数値を一様ランダムかつ独立に <var>M</var> 個生成する乱数生成器がある。</p>
<p>これらを使って以下のゲームを行う。</p>
<ol>
<li>最初に <var>1</var> 以上 <var>U</var> 以下の整数 <var>A_1, \dots, A_N</var> を自由に選び、<var>i</var> 番目のカードには <var>A_i</var> を書き込む</li>
<li>次に乱数生成器により <var>M</var> 個の整数 <var>B_1, \dots, B_M</var> が生成される</li>
<li><var>0</var> 枚以上の好きな枚数のカードを捨て、捨てなかったカードを <var>M</var> 個の山に分ける（空の山があってもよい）</li>
</ol>
<p>このゲームの目標は、<var>j</var> 番目の山のカードに書かれた数の合計を <var>B_j</var> に近くすることである。</p>
</section>
</div>

<div class="part">
<section>
<h3>得点</h3><p><var>j</var> 番目の山に含まれるカードに書かれた数の総和を <var>S_j</var> とし、誤差 <var>E</var> を <var>E = \sum_{j = 1}^M |S_j - B_j|</var> で定義する。
このとき得点は <var>\mathrm{round}((20 - \log_{10}(1 + E)) \times 5 \times 10^7)</var> である。</p>
<p>合計で 150 個のテストケースがあり、各テストケースの得点の合計が提出の得点となる。
一つ以上のテストケースで不正な出力や制限時間超過をした場合、提出全体の判定が<span class='label label-warning' data-toggle='tooltip' data-placement='top' title="不正解">WA</span>や<span class='label label-warning' data-toggle='tooltip' data-placement='top' title="実行時間制限超過">TLE</span>となる。
コンテスト時間中に得た最高得点で最終順位が決定され、コンテスト終了後のシステムテストは行われない。 同じ得点を複数の参加者が得た場合、提出時刻に関わらず同じ順位となる。</p>
</section>
</div>

<div class="part">
<section>
<h3>入出力</h3><p>最初に <var>N, M, L, U</var> が標準入力から与えられる。</p>
<pre><var>N</var> <var>M</var> <var>L</var> <var>U</var>
</pre>
<p><var>i</var> 番目のカードに書き込む数を <var>A_i</var> として、以下のように標準出力に出力せよ。</p>
<pre><var>A_1</var> <var>A_2</var> <var>\dots</var> <var>A_N</var>
</pre>
<p>その後、<var>B_1, \dots, B_M</var> が標準入力から与えられる。（<var>A_1, \dots, A_N</var> を出力するまでは与えられないことに注意せよ）</p>
<pre><var>B_1</var> <var>B_2</var> <var>\dots</var> <var>B_M</var>
</pre>
<p><var>i</var> 番目のカードを捨てるなら <var>X_i = 0</var>、そうでないなら <var>i</var> 番目のカードが属する山を <var>X_i</var> として、以下のように標準出力に出力せよ。</p>
<pre><var>X_1</var> <var>X_2</var> <var>\dots</var> <var>X_N</var>
</pre>
<p><font color="red"><strong>どの出力の後にも改行をし、更に標準出力を flush しなければならない。</strong></font>そうしない場合、<span class='label label-warning' data-toggle='tooltip' data-placement='top' title="実行時間制限超過">TLE</span>となる可能性がある。</p>
<p><a href="https://img.atcoder.jp/ahc053/Q405bDmv.html?lang=ja&seed=0&output=sample">例を見る</a></p>
</section>
</div>

<div class="part">
<section>
<h3>入力生成方法</h3><ul>
<li><var>N = 500, M = 50, L = 10^{15} - 2 \times 10^{12}, U = 10^{15} + 2 \times 10^{12}</var></li>
<li><var>L</var> 以上 <var>U</var> 以下の整数を独立かつ一様ランダムに <var>M</var> 個生成し、それらを昇順に並べたものを <var>B_1, \dots, B_M</var> とする</li>
</ul>
</section>
</div>

<div class="part">
<section>
<h3>ツール(入力ジェネレータ・ビジュアライザ)</h3><ul>
<li><a href="https://img.atcoder.jp/ahc053/Q405bDmv.html?lang=ja">Web版</a>: ローカル版より高性能でアニメーション表示が可能です。</li>
<li><a href="https://img.atcoder.jp/ahc053/Q405bDmv.zip">ローカル版</a>: 使用するには<a href="https://www.rust-lang.org/ja">Rust言語</a>のコンパイル環境をご用意下さい。<ul>
<li><a href="https://img.atcoder.jp/ahc053/Q405bDmv_windows.zip">Windows用のコンパイル済みバイナリ</a>: Rust言語の環境構築が面倒な方は代わりにこちらをご利用下さい。</li>
</ul>
</li>
</ul>
<p>コンテスト期間中に、ビジュアライズ結果の共有や、解法・考察に関する言及は禁止されています。ご注意下さい。</p>
</section>
</div>

<hr />
<div class="part">
<section>
<h3>入力例 1</h3><pre style="max-height:200px;overflow-y:scroll">500 50 998000000000000 1002000000000000
998048751022181 998097465238075 998160669164017 998231311927150 998239693029031 998356130354587 998446906912252 998555170329840 998587344209968 998743301391436 998760667153740 998947286002826 998981740663966 999002845061505 999059061039867 999128286657558 999297656665943 999358823245113 999383413043563 999640703784442 999733472254114 999790366912377 999873266864151 999911534238464 999985985322908 1000010039870477 1000155426700245 1000265729136158 1000462334004741 1000565907102777 1000908034069731 1000951689976892 1000957188499933 1001083800047313 1001089587866566 1001202738050092 1001213435143294 1001225125980911 1001264019997723 1001404093183272 1001438327970254 1001443079569383 1001470062137717 1001585047564559 1001647141491385 1001664377029572 1001719343473297 1001731992430657 1001856451123514 1001928070662947
</pre>
</section>
</div>

<div class="part">
<section>
<h3>出力例 1</h3><pre style="max-height:200px;overflow-y:scroll">4878135165674 172326132447595 37202234641402 179431630606157 22367690958241 99398565411526 17824324247087 29396577708895 113399743264687 145547105190040 64947716130460 8041491659091 186153140838491 161417555344578 163364200886004 38071391044721 182539431643788 93757006550779 11996636103012 183402070330078 89607863964449 27786275008532 69239822830324 49136120231533 95672288635106 107879106347250 186786221154355 160297039407090 173676609992710 123239816937288 147732083343410 160832428921840 56470747210805 82117224411351 128423650493987 154633872721597 165169521845991 70255087030182 186572488881683 85439423821558 58152155997326 113618217535512 23882336450073 29752867394401 40457796217587 88890100631976 181087482270344 179986532653780 186706357607984 162995007233097 51685901010151 34543683177733 184821641872661 9922581656873 118951828724908 172903355502154 158679627525888 137644767545187 1484850762556 196534224073290 121459351324523 168299978302008 60944453351792 98306564918571 98768117836360 32112998518513 22997838188543 16455218429428 184259712368439 95537956840974 20919714353586 72020282672271 196247456599359 73941973388422 149236730610450 52485558481602 43493879692637 111603487439157 91759098122681 106715798633341 25210134755121 168300981395261 68340092630266 7430989215844 255948408716 97897054411036 130970562942448 44234375773267 66688744234018 80363807275344 999759229847 78846688047077 35850416458440 118583097897305 41122108594995 116303652201362 100061923053820 82501051140921 133357886790151 55090967001663 131304026084121 136925409307094 173368272000661 41626860889009 121428782438741 115572283528675 146423877816698 138031240524283 6717105403927 96608107714415 181007758631780 163877461949056 58454026019942 173196381965721 83828254837946 27365965733613 123357685059832 152596661570638 168772870853853 183312512983498 72099524059729 156563719085081 37888171573095 86554156846695 93147007113874 171469180745630 14097649901745 31642505205254 713423490455 88132079701089 124545849345520 187043575495050 36946902864744 177309905024870 72530014930894 169674427938233 195621165200923 140576224915409 99274073814560 102951256656572 172002076869867 59678511021248 75194562569283 9626630468247 105910621886896 94062638028883 122389468398863 46609092476282 4985552734866 61080368992110 53508526864073 172110906291877 176877471908897 14298711632677 142156157487527 150831453521045 183576069482621 52371179625372 1412906974299 189653636558272 51220025173479 148975694622164 181724306775374 89744964521615 131295610053372 2590815627792 192328196506106 39337115429624 170946916963496 39099297719195 128886977076631 25857457400956 60828845467996 137937772601196 186539553326175 118628720437323 22740560815040 32127562955244 111376849651706 145257937590880 110723236219145 83330438465209 82268448952537 90779305894112 90066039098188 147095579827980 21270390659068 178279531945620 188702608162194 138970675846999 179199841047764 10505540671149 127263507906826 33779747700612 71591466780914 174617160129776 15220613853398 52591103564346 169360727859377 169108191893272 100132914534280 28723473275891 145508315896063 93814367491764 144414660116168 182070949734662 40102232584838 119636928819560 158407162710531 68625771826872 184870633286453 115922564933721 182922994791461 18516054760902 14288014986112 49731205356588 10556409317904 170574585419890 122955587097328 85732667255915 96356425915233 11774646353401 178614301383248 82558769381361 134697814075593 110565440004523 144177805777389 125842285793939 8465244499153 94053608125699 39611240421516 122771385439740 61418965802239 13803310002798 17153208186981 119693183026114 108963986033121 65747076614118 2671683167441 100014888277582 70208418523084 159454683955675 112790013691428 6328351676530 156349227922601 33481921959577 170856378368027 175855067043622 137190398191239 83044982447778 186978987219399 103958506918023 45093201844654 161534205407947 50558485129243 36756822846247 153489552211551 50976437431565 41742725652071 81157600953482 82743905702820 57463466072779 16864262492832 171031317985052 28552729328470 112418729957187 64856983187947 32283214334592 184882565676855 70921420401542 197531816179421 66260899495672 60031062654143 128561855849628 83296671384322 188072074415948 178231361352151 139601346027959 12824871094282 174441381840862 180581455884322 111422528264219 74908013766526 134746087433292 150824226252954 3035807639624 182056318780230 134568379722977 14996909923429 182622933284308 16733112125628 95372276370077 143158533546015 56806235477872 167805618225379 140635352935877 67576911563512 138808628160563 126060203974782 20634421177558 126346362865335 98784836561996 169028717310920 170932178231975 19172454215188 84386611637990 9255070357080 7975051479486 132340262678558 170774679947766 192104077767922 108745972848169 28251722938866 101231189697417 151177096307487 179791735550912 132045208186974 129700386641024 144403539116628 120079072529471 130711033054180 65505445939571 46016715076369 129171288926382 2890937548515 145968870552900 8473245816885 98721972867807 72808700369821 72363694581243 14219803286375 135982890036939 59216277883645 17875239026147 126050561885428 3793867036083 25069188909350 117454914636137 71825141386803 131938644075847 62919626819569 114044609539001 162386374310262 191558861552155 88417991497924 134350026539719 139240241374351 118122484902032 135969169383890 135383435476544 135236033638258 186001980751576 12331091837718 80965586517937 161492734633433 146948578829553 12322916148648 106326973402872 49157653543345 10248771076624 153621218916668 195198717780981 119314003745779 8260621590103 88788369013311 126948859664262 198030790502127 83796285967079 30731003849877 118036767880863 156966579482908 106944110944473 21556972876621 182306346940970 170450729202618 179189998727061 32945344398161 17666227026372 103276778263200 9981772578193 172192988856161 65323463579776 102185261498215 150486758294142 168272143224048 123666132438239 147950832737525 171402302238576 198700117919153 167290691980569 168129944540979 117552381019941 187585757097214 175141758043647 130145830770936 57991522347982 154471218782648 27219258762438 21367282114513 19070893688260 175793297364949 198958857319612 136337706007903 107891186620455 169338983484102 85474384580638 83482727117575 67994458181177 195451885960117 114463291839175 48014878655077 143818768310668 35873448699819 177890742562013 96335264389904 170866019123168 98181817198380 116006884690068 168203744273897 4339556785459 7240291505220 185055391999168 108456222200551 161118035855103 24197494785580 70383345957319 121822320201028 137697714709924 83491962095088 149793354643819 27194438908817 26043949921522 10769139969436 88529055839894 40746686837330 137444894997926 154611019704281 121037097818825 181201284000346 128259672114033 8234686502222 150406687151262 21828408625836 105236136708969 157017081160908 55151537773195 89897755449857 193497441845784 138839670436556 163754806508898 6130465987016 68846536751091 161010225663027 43787287494878 79370144078391 47716191961908 74151658516207 49351614882612 124933582980482 123710529331129 101337398892978 142083098747328 36743418081469 149196407314378 51944332582786 145669941720490 56597917020857 12426505177843 156071139797657 31098037318727 16899910384718 135564146864309 96155571382511 166761714979224 119660585662407 116556521900921 13075867757727 171927078030575 86516005760349 52334884486392 149027530548370 104989284110206 154360637696623 71358775898907 180832050301676 116945212143556 95494443632798 10760652483995 53199554778214 151072121271245 42638833672648 130633371745118 9959284633009 50002383926970 196370811167142 7381860299092 57108437698439 172561772310397 119680733426256 174053340728435
12 42 0 0 37 11 28 38 18 26 48 29 2 37 22 35 27 15 25 1 2 18 26 46 47 45 4 0 6 4 19 15 13 37 49 30 45 30 24 14 45 20 46 31 30 37 13 28 17 28 3 23 44 10 46 7 27 35 21 48 6 9 26 46 15 37 23 0 23 30 29 34 45 36 5 25 28 28 20 18 8 43 47 36 15 25 8 46 0 13 34 25 41 28 30 41 22 43 31 40 44 41 2 30 30 15 46 21 40 13 9 7 29 4 50 43 50 40 40 23 5 12 22 37 44 27 33 49 45 30 11 18 11 11 30 36 25 43 34 33 39 36 38 43 46 21 43 0 5 49 0 21 47 27 27 20 40 21 8 29 13 3 29 17 39 41 50 20 4 43 18 9 15 36 44 2 48 20 13 1 25 39 7 38 40 20 31 16 3 6 28 4 9 40 46 16 22 8 10 4 24 18 40 16 44 48 21 16 11 7 50 24 32 4 2 33 5 11 3 11 4 5 38 20 34 22 39 6 25 18 44 39 9 49 29 38 6 21 4 28 37 24 32 48 2 8 10 42 33 42 33 45 11 34 44 34 48 0 23 21 11 45 24 18 21 47 31 23 19 6 9 38 13 36 42 20 36 1 18 1 45 27 33 50 39 25 47 43 15 3 25 46 6 14 43 10 48 26 40 9 7 5 18 48 41 20 9 46 16 28 6 22 31 19 32 26 40 12 24 37 17 7 12 3 1 35 19 26 41 15 50 42 38 44 7 41 15 5 47 7 44 20 3 0 22 32 33 0 38 22 46 43 26 31 11 28 8 6 26 4 32 2 7 19 28 37 5 0 5 40 33 45 10 18 41 15 44 24 31 17 4 31 36 17 25 11 15 44 32 41 27 29 6 29 8 4 10 23 42 19 37 0 46 5 27 26 46 13 23 12 46 22 49 39 42 31 45 27 24 0 49 19 30 29 32 0 30 21 49 9 44 22 37 48 42 1 37 38 1 5 13 44 12 43 17 10 38 27 26 27 8 47 50 49 19 44 4 32 4 2 2 6 26 3 49 20 41 34 29 47 43 31 12 38 19 17 21 42 27 15 48 32 24 2 31 7 45 39 26 26 33 38 1 40 32 42 23 40 25 11
</pre></section>
</div>
</span>
<span class="lang-en">

<div class="part">
<section>
<h3>Problem Statement</h3><p>There are <var>N</var> blank cards, and a random number generator that generates <var>M</var> integers between <var>L</var> and <var>U</var>, inclusive, independently and uniformly at random.</p>
<p>Using these items, you play the following game.</p>
<ol>
<li>First you choose arbitrary integers <var>A_1, \dots, A_N</var> between <var>1</var> and <var>U</var>, inclusive, and write <var>A_i</var> on <var>i</var>-th card.</li>
<li>Then, using the random number generator, <var>M</var> integers <var>B_1, \dots, B_M</var> are generated.</li>
<li>You may discard any number of cards (possibly zero), and partition the remaining cards into <var>M</var> piles (possibly empty).</li>
</ol>
<p>The objective of this game is to make the sum of the integers written on cards in <var>j</var>-th pile as close to <var>B_j</var> as possible.</p>
</section>
</div>

<div class="part">
<section>
<h3>Scoring</h3><p>Let <var>S_j</var> be the sum of the integers written on cards in <var>j</var>-th pile, and define the error <var>E</var> by <var>E = \sum_{j = 1}^M |S_j - B_j|</var>.
Then your score is <var>\mathrm{round}((20 - \log_{10}(1 + E)) \times 5 \times 10^7)</var>.</p>
<p>There are <var>150</var> test cases, and the score of a submission is the total score for each test case.
If your submission produces an illegal output or exceeds the time limit for some test cases, the submission itself will be judged as <span class='label label-warning' data-toggle='tooltip' data-placement='top' title="Wrong Answer">WA</span> or <span class='label label-warning' data-toggle='tooltip' data-placement='top' title="Time Limit Exceeded">TLE</span> , and the score of the submission will be zero.
The highest score obtained during the contest will determine the final ranking, and there will be no system test after the contest.
If more than one participant gets the same score, they will be ranked in the same place regardless of the submission time.</p>
</section>
</div>

<div class="part">
<section>
<h3>Input and Output</h3><p>First, <var>N, M, L, U</var> are given from Standard Input.</p>
<pre><var>N</var> <var>M</var> <var>L</var> <var>U</var>
</pre>
<p>Let <var>A_i</var> be the integer you write on <var>i</var>-th card. Then output to Standard Output as follows.</p>
<pre><var>A_1</var> <var>A_2</var> <var>\dots</var> <var>A_N</var>
</pre>
<p>Then <var>B_1, \dots, B_M</var> are given from Standard Input. (Notice that you have to output <var>A_1, \dots, A_N</var> before reading <var>B_1, \dots, B_M</var>.)</p>
<pre><var>B_1</var> <var>B_2</var> <var>\dots</var> <var>B_M</var>
</pre>
<p>Let <var>X_i = 0</var> if you decided to discard <var>i</var>-th card, and otherwise let <var>X_i</var> be the index of the pile to which <var>i</var>-th card belongs.
Then output to Standard Output as follows.</p>
<pre><var>X_1</var> <var>X_2</var> <var>\dots</var> <var>X_N</var>
</pre>
<p><font color="red"><strong>All the output must be followed by a new line, and you have to flush Standard Output.</strong></font>
Otherwise, the submission might be judged as <span class='label label-warning' data-toggle='tooltip' data-placement='top' title="Time Limit Exceeded">TLE</span>.</p>
<p><a href="https://img.atcoder.jp/ahc053/Q405bDmv.html?lang=en&seed=0&output=sample">Show example</a></p>
</section>
</div>

<div class="part">
<section>
<h3>Input Generation</h3><ul>
<li><var>N = 500, M = 50, L = 10^{15} - 2 \times 10^{12}, U = 10^{15} + 2 \times 10^{12}</var></li>
<li>Generate <var>M</var> integers between <var>L</var> and <var>U</var>, inclusive, independently and uniformly at random, and let <var>B_1, \dots, B_M</var> be the sequence obtained by sorting them in ascending order.</li>
</ul>
</section>
</div>

<div class="part">
<section>
<h3>Tools (Input generator and visualizer)</h3><ul>
<li><a href="https://img.atcoder.jp/ahc053/Q405bDmv.html?lang=en">Web version</a>: This is more powerful than the local version providing animations.</li>
<li><a href="https://img.atcoder.jp/ahc053/Q405bDmv.zip">Local version</a>: You need a compilation environment of <a href="https://www.rust-lang.org/">Rust language</a>.<ul>
<li><a href="https://img.atcoder.jp/ahc053/Q405bDmv_windows.zip">Pre-compiled binary for Windows</a>: If you are not familiar with the Rust language environment, please use this instead.</li>
</ul>
</li>
</ul>
<p>Please be aware that sharing visualization results or discussing solutions/ideas during the contest is prohibited.</p>
</section>
</div>

<hr />
<div class="part">
<section>
<h3>Sample Input 1</h3><pre style="max-height:200px;overflow-y:scroll">500 50 998000000000000 1002000000000000
998048751022181 998097465238075 998160669164017 998231311927150 998239693029031 998356130354587 998446906912252 998555170329840 998587344209968 998743301391436 998760667153740 998947286002826 998981740663966 999002845061505 999059061039867 999128286657558 999297656665943 999358823245113 999383413043563 999640703784442 999733472254114 999790366912377 999873266864151 999911534238464 999985985322908 1000010039870477 1000155426700245 1000265729136158 1000462334004741 1000565907102777 1000908034069731 1000951689976892 1000957188499933 1001083800047313 1001089587866566 1001202738050092 1001213435143294 1001225125980911 1001264019997723 1001404093183272 1001438327970254 1001443079569383 1001470062137717 1001585047564559 1001647141491385 1001664377029572 1001719343473297 1001731992430657 1001856451123514 1001928070662947
</pre>
</section>
</div>

<div class="part">
<section>
<h3>Sample Output 1</h3><pre style="max-height:200px;overflow-y:scroll">4878135165674 172326132447595 37202234641402 179431630606157 22367690958241 99398565411526 17824324247087 29396577708895 113399743264687 145547105190040 64947716130460 8041491659091 186153140838491 161417555344578 163364200886004 38071391044721 182539431643788 93757006550779 11996636103012 183402070330078 89607863964449 27786275008532 69239822830324 49136120231533 95672288635106 107879106347250 186786221154355 160297039407090 173676609992710 123239816937288 147732083343410 160832428921840 56470747210805 82117224411351 128423650493987 154633872721597 165169521845991 70255087030182 186572488881683 85439423821558 58152155997326 113618217535512 23882336450073 29752867394401 40457796217587 88890100631976 181087482270344 179986532653780 186706357607984 162995007233097 51685901010151 34543683177733 184821641872661 9922581656873 118951828724908 172903355502154 158679627525888 137644767545187 1484850762556 196534224073290 121459351324523 168299978302008 60944453351792 98306564918571 98768117836360 32112998518513 22997838188543 16455218429428 184259712368439 95537956840974 20919714353586 72020282672271 196247456599359 73941973388422 149236730610450 52485558481602 43493879692637 111603487439157 91759098122681 106715798633341 25210134755121 168300981395261 68340092630266 7430989215844 255948408716 97897054411036 130970562942448 44234375773267 66688744234018 80363807275344 999759229847 78846688047077 35850416458440 118583097897305 41122108594995 116303652201362 100061923053820 82501051140921 133357886790151 55090967001663 131304026084121 136925409307094 173368272000661 41626860889009 121428782438741 115572283528675 146423877816698 138031240524283 6717105403927 96608107714415 181007758631780 163877461949056 58454026019942 173196381965721 83828254837946 27365965733613 123357685059832 152596661570638 168772870853853 183312512983498 72099524059729 156563719085081 37888171573095 86554156846695 93147007113874 171469180745630 14097649901745 31642505205254 713423490455 88132079701089 124545849345520 187043575495050 36946902864744 177309905024870 72530014930894 169674427938233 195621165200923 140576224915409 99274073814560 102951256656572 172002076869867 59678511021248 75194562569283 9626630468247 105910621886896 94062638028883 122389468398863 46609092476282 4985552734866 61080368992110 53508526864073 172110906291877 176877471908897 14298711632677 142156157487527 150831453521045 183576069482621 52371179625372 1412906974299 189653636558272 51220025173479 148975694622164 181724306775374 89744964521615 131295610053372 2590815627792 192328196506106 39337115429624 170946916963496 39099297719195 128886977076631 25857457400956 60828845467996 137937772601196 186539553326175 118628720437323 22740560815040 32127562955244 111376849651706 145257937590880 110723236219145 83330438465209 82268448952537 90779305894112 90066039098188 147095579827980 21270390659068 178279531945620 188702608162194 138970675846999 179199841047764 10505540671149 127263507906826 33779747700612 71591466780914 174617160129776 15220613853398 52591103564346 169360727859377 169108191893272 100132914534280 28723473275891 145508315896063 93814367491764 144414660116168 182070949734662 40102232584838 119636928819560 158407162710531 68625771826872 184870633286453 115922564933721 182922994791461 18516054760902 14288014986112 49731205356588 10556409317904 170574585419890 122955587097328 85732667255915 96356425915233 11774646353401 178614301383248 82558769381361 134697814075593 110565440004523 144177805777389 125842285793939 8465244499153 94053608125699 39611240421516 122771385439740 61418965802239 13803310002798 17153208186981 119693183026114 108963986033121 65747076614118 2671683167441 100014888277582 70208418523084 159454683955675 112790013691428 6328351676530 156349227922601 33481921959577 170856378368027 175855067043622 137190398191239 83044982447778 186978987219399 103958506918023 45093201844654 161534205407947 50558485129243 36756822846247 153489552211551 50976437431565 41742725652071 81157600953482 82743905702820 57463466072779 16864262492832 171031317985052 28552729328470 112418729957187 64856983187947 32283214334592 184882565676855 70921420401542 197531816179421 66260899495672 60031062654143 128561855849628 83296671384322 188072074415948 178231361352151 139601346027959 12824871094282 174441381840862 180581455884322 111422528264219 74908013766526 134746087433292 150824226252954 3035807639624 182056318780230 134568379722977 14996909923429 182622933284308 16733112125628 95372276370077 143158533546015 56806235477872 167805618225379 140635352935877 67576911563512 138808628160563 126060203974782 20634421177558 126346362865335 98784836561996 169028717310920 170932178231975 19172454215188 84386611637990 9255070357080 7975051479486 132340262678558 170774679947766 192104077767922 108745972848169 28251722938866 101231189697417 151177096307487 179791735550912 132045208186974 129700386641024 144403539116628 120079072529471 130711033054180 65505445939571 46016715076369 129171288926382 2890937548515 145968870552900 8473245816885 98721972867807 72808700369821 72363694581243 14219803286375 135982890036939 59216277883645 17875239026147 126050561885428 3793867036083 25069188909350 117454914636137 71825141386803 131938644075847 62919626819569 114044609539001 162386374310262 191558861552155 88417991497924 134350026539719 139240241374351 118122484902032 135969169383890 135383435476544 135236033638258 186001980751576 12331091837718 80965586517937 161492734633433 146948578829553 12322916148648 106326973402872 49157653543345 10248771076624 153621218916668 195198717780981 119314003745779 8260621590103 88788369013311 126948859664262 198030790502127 83796285967079 30731003849877 118036767880863 156966579482908 106944110944473 21556972876621 182306346940970 170450729202618 179189998727061 32945344398161 17666227026372 103276778263200 9981772578193 172192988856161 65323463579776 102185261498215 150486758294142 168272143224048 123666132438239 147950832737525 171402302238576 198700117919153 167290691980569 168129944540979 117552381019941 187585757097214 175141758043647 130145830770936 57991522347982 154471218782648 27219258762438 21367282114513 19070893688260 175793297364949 198958857319612 136337706007903 107891186620455 169338983484102 85474384580638 83482727117575 67994458181177 195451885960117 114463291839175 48014878655077 143818768310668 35873448699819 177890742562013 96335264389904 170866019123168 98181817198380 116006884690068 168203744273897 4339556785459 7240291505220 185055391999168 108456222200551 161118035855103 24197494785580 70383345957319 121822320201028 137697714709924 83491962095088 149793354643819 27194438908817 26043949921522 10769139969436 88529055839894 40746686837330 137444894997926 154611019704281 121037097818825 181201284000346 128259672114033 8234686502222 150406687151262 21828408625836 105236136708969 157017081160908 55151537773195 89897755449857 193497441845784 138839670436556 163754806508898 6130465987016 68846536751091 161010225663027 43787287494878 79370144078391 47716191961908 74151658516207 49351614882612 124933582980482 123710529331129 101337398892978 142083098747328 36743418081469 149196407314378 51944332582786 145669941720490 56597917020857 12426505177843 156071139797657 31098037318727 16899910384718 135564146864309 96155571382511 166761714979224 119660585662407 116556521900921 13075867757727 171927078030575 86516005760349 52334884486392 149027530548370 104989284110206 154360637696623 71358775898907 180832050301676 116945212143556 95494443632798 10760652483995 53199554778214 151072121271245 42638833672648 130633371745118 9959284633009 50002383926970 196370811167142 7381860299092 57108437698439 172561772310397 119680733426256 174053340728435
12 42 0 0 37 11 28 38 18 26 48 29 2 37 22 35 27 15 25 1 2 18 26 46 47 45 4 0 6 4 19 15 13 37 49 30 45 30 24 14 45 20 46 31 30 37 13 28 17 28 3 23 44 10 46 7 27 35 21 48 6 9 26 46 15 37 23 0 23 30 29 34 45 36 5 25 28 28 20 18 8 43 47 36 15 25 8 46 0 13 34 25 41 28 30 41 22 43 31 40 44 41 2 30 30 15 46 21 40 13 9 7 29 4 50 43 50 40 40 23 5 12 22 37 44 27 33 49 45 30 11 18 11 11 30 36 25 43 34 33 39 36 38 43 46 21 43 0 5 49 0 21 47 27 27 20 40 21 8 29 13 3 29 17 39 41 50 20 4 43 18 9 15 36 44 2 48 20 13 1 25 39 7 38 40 20 31 16 3 6 28 4 9 40 46 16 22 8 10 4 24 18 40 16 44 48 21 16 11 7 50 24 32 4 2 33 5 11 3 11 4 5 38 20 34 22 39 6 25 18 44 39 9 49 29 38 6 21 4 28 37 24 32 48 2 8 10 42 33 42 33 45 11 34 44 34 48 0 23 21 11 45 24 18 21 47 31 23 19 6 9 38 13 36 42 20 36 1 18 1 45 27 33 50 39 25 47 43 15 3 25 46 6 14 43 10 48 26 40 9 7 5 18 48 41 20 9 46 16 28 6 22 31 19 32 26 40 12 24 37 17 7 12 3 1 35 19 26 41 15 50 42 38 44 7 41 15 5 47 7 44 20 3 0 22 32 33 0 38 22 46 43 26 31 11 28 8 6 26 4 32 2 7 19 28 37 5 0 5 40 33 45 10 18 41 15 44 24 31 17 4 31 36 17 25 11 15 44 32 41 27 29 6 29 8 4 10 23 42 19 37 0 46 5 27 26 46 13 23 12 46 22 49 39 42 31 45 27 24 0 49 19 30 29 32 0 30 21 49 9 44 22 37 48 42 1 37 38 1 5 13 44 12 43 17 10 38 27 26 27 8 47 50 49 19 44 4 32 4 2 2 6 26 3 49 20 41 34 29 47 43 31 12 38 19 17 21 42 27 15 48 32 24 2 31 7 45 39 26 26 33 38 1 40 32 42 23 40 25 11
</pre></section>
</div>
</span>
</span>

		</div>

		

		
		<hr/>
		<form class="form-horizontal form-code-submit" action="/contests/ahc053/submit" method="POST">
			<input type="hidden" name="data.TaskScreenName" value="ahc053_a" />
			
			<div class="form-group ">
				<label class="control-label col-sm-3 col-md-2" for="select-lang">言語</label>
				<div id="select-lang" class="col-sm-5" data-name="data.LanguageId">
					<select class="form-control current" data-placeholder="-" name="data.LanguageId" required>
						<option></option>
						
							<option value="5001" data-ace-mode="c_cpp">C&#43;&#43; 20 (gcc 12.2)</option>
						
							<option value="5002" data-ace-mode="golang">Go (go 1.20.6)</option>
						
							<option value="5003" data-ace-mode="csharp">C# 11.0 (.NET 7.0.7)</option>
						
							<option value="5004" data-ace-mode="kotlin">Kotlin (Kotlin/JVM 1.8.20)</option>
						
							<option value="5005" data-ace-mode="java">Java (OpenJDK 17)</option>
						
							<option value="5006" data-ace-mode="nim">Nim (Nim 1.6.14)</option>
						
							<option value="5007" data-ace-mode="text">V (V 0.4)</option>
						
							<option value="5008" data-ace-mode="text">Zig (Zig 0.10.1)</option>
						
							<option value="5009" data-ace-mode="javascript">JavaScript (Node.js 18.16.1)</option>
						
							<option value="5010" data-ace-mode="javascript">JavaScript (Deno 1.35.1)</option>
						
							<option value="5011" data-ace-mode="r">R (GNU R 4.2.1)</option>
						
							<option value="5012" data-ace-mode="d">D (DMD 2.104.0)</option>
						
							<option value="5013" data-ace-mode="d">D (LDC 1.32.2)</option>
						
							<option value="5014" data-ace-mode="swift">Swift (swift 5.8.1)</option>
						
							<option value="5015" data-ace-mode="dart">Dart (Dart 3.0.5)</option>
						
							<option value="5016" data-ace-mode="php">PHP (php 8.2.8)</option>
						
							<option value="5017" data-ace-mode="c_cpp">C (gcc 12.2.0)</option>
						
							<option value="5018" data-ace-mode="ruby">Ruby (ruby 3.2.2)</option>
						
							<option value="5019" data-ace-mode="crystal">Crystal (Crystal 1.9.1)</option>
						
							<option value="5020" data-ace-mode="text">Brainfuck (bf 20041219)</option>
						
							<option value="5021" data-ace-mode="fsharp">F# 7.0 (.NET 7.0.7)</option>
						
							<option value="5022" data-ace-mode="julia">Julia (Julia 1.9.2)</option>
						
							<option value="5023" data-ace-mode="sh">Bash (bash 5.2.2)</option>
						
							<option value="5024" data-ace-mode="text">Text (cat 8.32)</option>
						
							<option value="5025" data-ace-mode="haskell">Haskell (GHC 9.4.5)</option>
						
							<option value="5026" data-ace-mode="fortran">Fortran (gfortran 12.2)</option>
						
							<option value="5027" data-ace-mode="lua">Lua (LuaJIT 2.1.0-beta3)</option>
						
							<option value="5028" data-ace-mode="c_cpp">C&#43;&#43; 23 (gcc 12.2)</option>
						
							<option value="5029" data-ace-mode="lisp">Common Lisp (SBCL 2.3.6)</option>
						
							<option value="5030" data-ace-mode="cobol">COBOL (Free) (GnuCOBOL 3.1.2)</option>
						
							<option value="5031" data-ace-mode="c_cpp">C&#43;&#43; 23 (Clang 16.0.6)</option>
						
							<option value="5032" data-ace-mode="sh">Zsh (Zsh 5.9)</option>
						
							<option value="5033" data-ace-mode="python">SageMath (SageMath 9.5)</option>
						
							<option value="5034" data-ace-mode="sh">Sed (GNU sed 4.8)</option>
						
							<option value="5035" data-ace-mode="text">bc (bc 1.07.1)</option>
						
							<option value="5036" data-ace-mode="text">dc (dc 1.07.1)</option>
						
							<option value="5037" data-ace-mode="perl">Perl (perl  5.34)</option>
						
							<option value="5038" data-ace-mode="sh">AWK (GNU Awk 5.0.1)</option>
						
							<option value="5039" data-ace-mode="text">なでしこ (cnako3 3.4.20)</option>
						
							<option value="5040" data-ace-mode="text">Assembly x64 (NASM 2.15.05)</option>
						
							<option value="5041" data-ace-mode="pascal">Pascal (fpc 3.2.2)</option>
						
							<option value="5042" data-ace-mode="csharp">C# 11.0 AOT (.NET 7.0.7)</option>
						
							<option value="5043" data-ace-mode="lua">Lua (Lua 5.4.6)</option>
						
							<option value="5044" data-ace-mode="prolog">Prolog (SWI-Prolog 9.0.4)</option>
						
							<option value="5045" data-ace-mode="sh">PowerShell (PowerShell 7.3.1)</option>
						
							<option value="5046" data-ace-mode="scheme">Scheme (Gauche 0.9.12)</option>
						
							<option value="5047" data-ace-mode="scala">Scala 3.3.0 (Scala Native 0.4.14)</option>
						
							<option value="5048" data-ace-mode="vbscript">Visual Basic 16.9 (.NET 7.0.7)</option>
						
							<option value="5049" data-ace-mode="text">Forth (gforth 0.7.3)</option>
						
							<option value="5050" data-ace-mode="clojure">Clojure (babashka 1.3.181)</option>
						
							<option value="5051" data-ace-mode="erlang">Erlang (Erlang 26.0.2)</option>
						
							<option value="5052" data-ace-mode="typescript">TypeScript 5.1 (Deno 1.35.1)</option>
						
							<option value="5053" data-ace-mode="c_cpp">C&#43;&#43; 17 (gcc 12.2)</option>
						
							<option value="5054" data-ace-mode="rust">Rust (rustc 1.70.0)</option>
						
							<option value="5055" data-ace-mode="python">Python (CPython 3.11.4)</option>
						
							<option value="5056" data-ace-mode="scala">Scala (Dotty 3.3.0)</option>
						
							<option value="5057" data-ace-mode="text">Koka (koka 2.4.0)</option>
						
							<option value="5058" data-ace-mode="typescript">TypeScript 5.1 (Node.js 18.16.1)</option>
						
							<option value="5059" data-ace-mode="ocaml">OCaml (ocamlopt 5.0.0)</option>
						
							<option value="5060" data-ace-mode="raku">Raku (Rakudo 2023.06)</option>
						
							<option value="5061" data-ace-mode="text">Vim (vim 9.0.0242)</option>
						
							<option value="5062" data-ace-mode="lisp">Emacs Lisp (Native Compile) (GNU Emacs 28.2)</option>
						
							<option value="5063" data-ace-mode="python">Python (Mambaforge / CPython 3.10.10)</option>
						
							<option value="5064" data-ace-mode="clojure">Clojure (clojure 1.11.1)</option>
						
							<option value="5065" data-ace-mode="text">プロデル (mono版プロデル 1.9.1182)</option>
						
							<option value="5066" data-ace-mode="text">ECLiPSe (ECLiPSe 7.1_13)</option>
						
							<option value="5067" data-ace-mode="text">Nibbles (literate form) (nibbles 1.01)</option>
						
							<option value="5068" data-ace-mode="ada">Ada (GNAT 12.2)</option>
						
							<option value="5069" data-ace-mode="text">jq (jq 1.6)</option>
						
							<option value="5070" data-ace-mode="text">Cyber (Cyber v0.2-Latest)</option>
						
							<option value="5071" data-ace-mode="clojure">Carp (Carp 0.5.5)</option>
						
							<option value="5072" data-ace-mode="c_cpp">C&#43;&#43; 17 (Clang 16.0.6)</option>
						
							<option value="5073" data-ace-mode="c_cpp">C&#43;&#43; 20 (Clang 16.0.6)</option>
						
							<option value="5074" data-ace-mode="text">LLVM IR (Clang 16.0.6)</option>
						
							<option value="5075" data-ace-mode="lisp">Emacs Lisp (Byte Compile) (GNU Emacs 28.2)</option>
						
							<option value="5076" data-ace-mode="text">Factor (Factor 0.98)</option>
						
							<option value="5077" data-ace-mode="d">D (GDC 12.2)</option>
						
							<option value="5078" data-ace-mode="python">Python (PyPy 3.10-v7.3.12)</option>
						
							<option value="5079" data-ace-mode="text">Whitespace (whitespacers 1.0.0)</option>
						
							<option value="5080" data-ace-mode="text">&gt;&lt;&gt; (fishr 0.1.0)</option>
						
							<option value="5081" data-ace-mode="ocaml">ReasonML (reason 3.9.0)</option>
						
							<option value="5082" data-ace-mode="python">Python (Cython 0.29.34)</option>
						
							<option value="5083" data-ace-mode="matlab">Octave (GNU Octave 8.2.0)</option>
						
							<option value="5084" data-ace-mode="haxe">Haxe (JVM) (Haxe 4.3.1)</option>
						
							<option value="5085" data-ace-mode="elixir">Elixir (Elixir 1.15.2)</option>
						
							<option value="5086" data-ace-mode="text">Mercury (Mercury 22.01.6)</option>
						
							<option value="5087" data-ace-mode="text">Seed7 (Seed7 3.2.1)</option>
						
							<option value="5088" data-ace-mode="lisp">Emacs Lisp (No Compile) (GNU Emacs 28.2)</option>
						
							<option value="5089" data-ace-mode="text">Unison (Unison M5b)</option>
						
							<option value="5090" data-ace-mode="cobol">COBOL (GnuCOBOL(Fixed) 3.1.2)</option>
						
					</select>
					<span class="error"></span>
				</div>
			</div>
			<script>var currentLang = getLS('defaultLang');</script>
			
			
<div class="form-group">
	<div class="col-sm-3 col-md-2 editor-label">
		<label class="control-label" for="sourceCode">ソースコード</label>
		<div class="editor-buttons">
			<button id="btn-open-file" type="button" class="btn btn-default btn-sm">
				<span class="glyphicon glyphicon-folder-open" aria-hidden="true"></span> &nbsp; ファイルを開く
			</button>
			<button id="btn-customize" type="button" class="btn btn-default btn-sm">
				<span class="glyphicon glyphicon-cog" aria-hidden="true"></span> &nbsp; カスタマイズ
			</button>
			<button type="button" class="btn btn-default btn-sm btn-toggle-editor" data-toggle="button" autocomplete="off">
				エディタ切り替え
			</button>
			<button type="button" class="btn btn-default btn-sm btn-auto-height" data-toggle="button" autocomplete="off">
				高さ自動調節
			</button>
		</div>
	</div>
	<div class="col-sm-9 col-md-10" id="sourceCode">
		<div id="editor"></div>
		<textarea id="plain-textarea" class="form-control" style="display:none;" name="sourceCode"></textarea>
		<p>
			<span class="gray">※ 512 KiB まで</span><br>
		</p>
	</div>
	<input id="input-open-file" type="file" style="display:none;">
</div>


			<input type="hidden" name="csrf_token" value="3dL3gwWP0TO1UYLmPM5t8gXd79YLsVlgYmVS/1&#43;Aax0=" />
			<div class="form-group">
				<label class="control-label col-sm-3 col-md-2"></label>
				<div class="col-sm-5">
					
					<button type="submit" class="btn btn-primary" id="submit">提出</button>
				</div>
			</div>
		</form>
		
	</div>
</div>




		
			<hr>
			
			
			
<div class="a2a_kit a2a_kit_size_20 a2a_default_style pull-right" data-a2a-url="https://atcoder.jp/contests/ahc053/tasks/ahc053_a?lang=ja" data-a2a-title="A - Random Sum Game">
	<a class="a2a_button_facebook"></a>
	<a class="a2a_button_twitter"></a>
	
		<a class="a2a_button_hatena"></a>
	
	<a class="a2a_dd" href="https://www.addtoany.com/share"></a>
</div>

		
		<script async src="//static.addtoany.com/menu/page.js"></script>
		
	</div> 
	<hr>
</div> 

	<div class="container" style="margin-bottom: 80px;">
			<footer class="footer">
			
				<ul>
					<li><a href="/contests/ahc053/rules">ルール</a></li>
					<li><a href="/contests/ahc053/glossary">用語集</a></li>
					
				</ul>
			
			<ul>
				<li><a href="/tos">利用規約</a></li>
				<li><a href="/privacy">プライバシーポリシー</a></li>
				<li><a href="/personal">個人情報保護方針</a></li>
				<li><a href="/company">企業情報</a></li>
				<li><a href="/faq">よくある質問</a></li>
				<li><a href="/contact">お問い合わせ</a></li>
				<li><a href="/documents/request">資料請求</a></li>
			</ul>
			<div class="text-center">
					<small id="copyright">Copyright Since 2012 &copy;<a href="http://atcoder.co.jp">AtCoder Inc.</a> All rights reserved.</small>
			</div>
			</footer>
	</div>
	<p id="fixed-server-timer" class="contest-timer"></p>
	<div id="scroll-page-top" style="display:none;"><span class="glyphicon glyphicon-arrow-up" aria-hidden="true"></span> ページトップ</div>

</body>
</html>