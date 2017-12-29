using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BookOfTwelve: WritingObject {

    public BookOfTwelve() {
        this.o_name = "Book of Twelve";
        this.o_desription = "A great tome bound in dry, thick leather. As you finger through the wide leaves, you find them lined by queer bookstaves you cannot read, " +
            "or even recall ever having seen before.\nThough the book is large indeed, you notice it feels much heavier than it should. Its front however bears a overline " +
            "writ in your tongue; it reads \"The Book of the Twelve\". On the first leaf, you see further writing that marks an inleading.";

        this.contents = "\"It may seem odd to ye, reader, that I should, instead of oversetting the U'um holy-writ into our tongue of Marnish, simply put forth its " +
            "untouched words as they were first chisled into the Ovannish stones, yet to this I would say that many such oversettings already litter the halls of " +
            "the worship-houses, and many written by better men than I. My labour is a different one. True it is that the U'um tongue is a difficult one, but only " +
            "through it may the Twelve Gods best be known.\"";
    }

}
