<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="VideoStats.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Video Stats Pages</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href="css/jumbotron.css" rel="stylesheet" />
     <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    
   <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">BallBall Video Stats</a>
        </div>
        <div class="navbar-collapse collapse">
          <%--<form class="navbar-form navbar-right" role="form">
            <div class="form-group">
              <input type="text" placeholder="Email" class="form-control">
            </div>
            <div class="form-group">
              <input type="password" placeholder="Password" class="form-control">
            </div>
            <button type="submit" class="btn btn-success">Sign in</button>
          </form>--%>
        </div><!--/.navbar-collapse -->
      </div>
    </div>

    <!-- Main jumbotron for a primary marketing message or call to action -->
    <div class="jumbotron">
      <div class="container">
        <h1>All Video Stats</h1>
        <p>This is a template for a simple marketing or informational website. It includes a large callout called a jumbotron and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
        <p><a class="btn btn-primary btn-lg" role="button">Learn more &raquo;</a></p>
      </div>
    </div>

    <div class="container">
      <!-- Example row of columns -->
      <div class="row">
        <div class="col-md-4">
          <h2>BPL</h2>
          <p>Video Stats for BPL clips. Includes Highlights and In Game Clips</p>
          <p><a class="btn btn-default" href="BPL/index.aspx" role="button">View details&raquo;</a></p>
        </div>
        <div class="col-md-2">
          <h2>La Liga</h2>
          <p>Video Stats for La Liga clips. Includes Highlights</p>
          <p><a class="btn btn-default" href="LaLiga/index.aspx" role="button">View details &raquo;</a></p>
       </div>
        <div class="col-md-2">
          <h2>Bundesliga</h2>
          <p>Video Stats for  Bundesliga. Includes Highlights</p>
          <p><a class="btn btn-default" href="Bundesliga/index.aspx" role="button">View details &raquo;</a></p>
        </div>
           <div class="col-md-2">
          <h2>Serie A</h2>
          <p>Video Stats for  Serie A. Includes Highlights</p>
          <p><a class="btn btn-default" href="SerieA/index.aspx" role="button">View details &raquo;</a></p>
        </div>
          <div class="col-md-2">
          <h2>Ligue 1</h2>
          <p>Video Stats for Ligue 1. Includes Highlights</p>
          <p><a class="btn btn-default" href="Ligue1/index.aspx" role="button">View details &raquo;</a></p>
        </div>
      </div>

      <hr/>

      <footer>
        <p>&copy; Company 2013</p>
      </footer>
    </div> <!-- /container -->


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
   
</body>
</html>
