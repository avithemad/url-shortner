import $ from "jquery";

const urlShortnerApi = window.origin + "/shortner";

function isValidHttpUrl(string) {
  let url;

  try {
    url = new URL(string);
  } catch (_) {
    return false;
  }

  return url.protocol === "http:" || url.protocol === "https:";
}

$(() => {
  $(".get-short-url-button").on("click", () => {
    var inpUrl = $(".get-short-url-input").val();
    if (isValidHttpUrl(inpUrl)) {
      $(".url-shortener-form .invalid-error").fadeOut(
        300,
        $(".url-shortener-form .invalid-error").remove
      );
      $(".url-shortener-form .short-url-result").remove();

      var jsonData = `{ "longUrl" : "${inpUrl}" }`;
      $.ajax({
        url: urlShortnerApi,
        type: "POST",
        dataType: "json",
        data: jsonData,
        contentType: "application/json",
      }).done((data) => {
        $(".url-shortener-form").append(
          $(`<div class="short-url-result">
                <p class="short-url">${data.shortUrl} </p> <span><button class="copy-button">COPY</button></span>
            </div>`)
            .hide()
            .fadeIn(300)
        );
        $(".copy-button").on("click", () => {
          /* Get the text field */
          var copyText = $(".short-url").text();
          console.log(copyText);

          /* Copy the text inside the text field */
          navigator.clipboard.writeText(copyText);
        });
      });
    } else {
      $(".url-shortener-form .invalid-error").remove();
      $(".url-shortener-form .short-url-result").remove();
      $(".url-shortener-form").append(
        $(`<div class="invalid-error">
            <p>Invalid URL entered, please enter a valid one.</p>
        </div>`)
          .hide()
          .fadeIn(300)
      );
      console.error("Not a valid url");
    }
  });
});
